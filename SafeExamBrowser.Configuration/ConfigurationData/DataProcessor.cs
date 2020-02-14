﻿/*
 * Copyright (c) 2020 ETH Zürich, Educational Development and Technology (LET)
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using SafeExamBrowser.Settings;

namespace SafeExamBrowser.Configuration.ConfigurationData
{
	internal class DataProcessor
	{
		internal void Process(IDictionary<string, object> rawData, AppSettings settings)
		{
			CalculateHashValue(rawData, settings);
		}

		private void CalculateHashValue(IDictionary<string, object> rawData, AppSettings settings)
		{
			using (var algorithm = new SHA256Managed())
			using (var stream = new MemoryStream())
			using (var writer = new StreamWriter(stream))
			{
				Serialize(rawData, writer);

				writer.Flush();
				stream.Seek(0, SeekOrigin.Begin);

				var hash = algorithm.ComputeHash(stream);
				var key = BitConverter.ToString(hash).ToLower().Replace("-", string.Empty);

				settings.Browser.ConfigurationKey = key;
			}
		}

		private void Serialize(IDictionary<string, object> dictionary, StreamWriter stream)
		{
			var orderedByKey = dictionary.OrderBy(d => d.Key, StringComparer.InvariantCulture).ToList();

			stream.Write('{');

			foreach (var kvp in orderedByKey)
			{
				var process = true;

				process &= !kvp.Key.Equals(Keys.General.OriginatorVersion, StringComparison.OrdinalIgnoreCase);
				process &= !(kvp.Value is IDictionary<string, object> d) || d.Any();

				if (process)
				{
					stream.Write('"');
					stream.Write(kvp.Key);
					stream.Write('"');
					stream.Write(':');
					Serialize(kvp.Value, stream);

					if (kvp.Key != orderedByKey.Last().Key)
					{
						stream.Write(',');
					}
				}
			}

			stream.Write('}');
		}

		private void Serialize(IList<object> list, StreamWriter stream)
		{
			stream.Write('[');

			foreach (var item in list)
			{
				Serialize(item, stream);

				if (item != list.Last())
				{
					stream.Write(',');
				}
			}

			stream.Write(']');
		}

		private void Serialize(object value, StreamWriter stream)
		{
			switch (value)
			{
				case IDictionary<string, object> dictionary:
					Serialize(dictionary, stream);
					break;
				case IList<object> list:
					Serialize(list, stream);
					break;
				case byte[] data:
					stream.Write('"');
					stream.Write(Convert.ToBase64String(data));
					stream.Write('"');
					break;
				case DateTime date:
					stream.Write(date.ToString("o"));
					break;
				case bool boolean:
					stream.Write(boolean.ToString().ToLower());
					break;
				case int integer:
					stream.Write(integer.ToString(NumberFormatInfo.InvariantInfo));
					break;
				case double number:
					stream.Write(number.ToString(NumberFormatInfo.InvariantInfo));
					break;
				case string text:
					stream.Write('"');
					stream.Write(text);
					stream.Write('"');
					break;
				case null:
					stream.Write('"');
					stream.Write('"');
					break;
			}
		}
	}
}