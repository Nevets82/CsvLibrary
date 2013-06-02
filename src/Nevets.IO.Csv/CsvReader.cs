namespace Nevets.IO.Csv
{
    /// <summary>
    /// Represents a Comma (Character) Seperated Value reader.
    /// </summary>
    public class CsvReader :
        System.IO.StreamReader
    {
        #region Field members.
        /// <summary>
        /// Contains the CSV header.
        /// </summary>
        private string[] _header;

        /// <summary>
        /// Contains the CSV reader options.
        /// </summary>
        private CsvOptions _options;
        #endregion

        #region Property members.
        /// <summary>
        /// Gets the CSV header.
        /// </summary>
        public string[] Header
        {
            get
            {
                if (this._header == null)
                {
                    this._header = this.ReadHeader();
                }

                return this._header;
            }
        }

        /// <summary>
        /// Gets the CSV reader options.
        /// </summary>
        public CsvOptions Options
        {
            get { return this._options; }
        }
        #endregion

        #region Public reader members.
        /// <summary>
        /// Reads a CSV record.
        /// </summary>
        /// <returns>A CSV record.</returns>
        public string[] ReadRecord()
        {
            var result = this.ReadRecords(1);

            if (result == null || result.Length == 0)
            {
                return null;
            }

            return result[0];
        }

        /// <summary>
        /// Reads an array of CSV records.
        /// </summary>
        /// <returns>An array of CSV records.</returns>
        public string[][] ReadRecords(int numberOfRecords)
        {
            if (this.Options.HasHeader && this._header == null)
            {
                this._header = this.ReadHeader();
            }

            var result = new System.Collections.Generic.List<string[]>();

            for (int index = 0; index < numberOfRecords; index++)
            {
                var record = this.ReadRecordFromStream();

                if (record == null)
                {
                    break;
                }

                result.Add(record);
            }

            return result.ToArray();
        }
        #endregion

        #region Private reader members.
        /// <summary>
        /// Reads the CSV header.
        /// </summary>
        /// <returns>The CSV header.</returns>
        private string[] ReadHeader()
        {
            string[] header = null;

            if (this.Options.HasHeader)
            {
                header = this.ReadRecordFromStream();
            }

            return header;
        }

        /// <summary>
        /// Reads a CSV record from the underliing stream.
        /// </summary>
        /// <returns>A CSV record.</returns>
        private string[] ReadRecordFromStream()
        {
            if (this.Peek() > -1)
            {
                string[] result = null;

                var record = this.ReadLine();

                while (!this.TryParseRecord(record, out result))
                {
                    record += "\r\n" + this.ReadLine();
                }

                return result;
            }

            return null;
        }

        /// <summary>
        /// Tries to parse the specified record to a string array.
        /// </summary>
        /// <param name="record">The record to parse.</param>
        /// <param name="result">The result.</param>
        /// <returns>True if the operation was successful, otherwise false.</returns>
        private bool TryParseRecord(string record, out string[] result)
        {
            var resultList = new System.Collections.Generic.List<string>();

            var values = record.Split(this.Options.Seperator);

            string value = null;

            for (int index = 0; index < values.Length; index++)
            {
                if (value == null)
                {
                    value = values[index];
                }
                else
                {
                    value += this.Options.Seperator + values[index];
                }

                if (this.IsValidValue(value))
                {
                    resultList.Add(this.Escape(value));

                    value = null;
                }
            }

            if (value == null)
            {
                result = resultList.ToArray();

                return true;
            }

            result = null;

            return false;
        }

        /// <summary>
        /// Returns true if the specified value is valid.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>True if the specified value is valid, otherwise false.</returns>
        private bool IsValidValue(string value)
        {
            var stringCharacter = this.Options.StringCharacter.ToString();

            if (this.HasValidStringCharacters(value))
            {
                if (value.StartsWith(stringCharacter))
                {
                    return value.EndsWith(stringCharacter);
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns true if the specified value has a valid number of string characters.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>True if the specified value has a valid number of string characters, otherwise false.</returns>
        private bool HasValidStringCharacters(string value)
        {
            var count = 0;

            var index = value.IndexOf(this.Options.StringCharacter);

            while (index >= 0)
            {
                count++;

                index = value.IndexOf(this.Options.StringCharacter, index + 1);
            }

            return count % 2 == 0;
        }

        /// <summary>
        /// Escapes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The escaped value.</returns>
        private string Escape(string value)
        {
            var stringCharacter = this.Options.StringCharacter.ToString();

            if (value == stringCharacter) { throw new System.ArgumentException(string.Format("The specified value cannot be '{0}'.", stringCharacter), "value"); }

            if (value.StartsWith(stringCharacter) && value.EndsWith(stringCharacter))
            {
                value = value.Substring(1, value.Length - 2);

                value = value.Replace(stringCharacter + stringCharacter, stringCharacter);
            }

            return value;
        }
        #endregion

        #region Constructor and deconstructor members.
        /// <summary>
        /// Creates a new instance of this class.
        /// </summary>
        public CsvReader(string path) :
            this(path, new CsvOptions())
        {
        }

        /// <summary>
        /// Creates a new instance of this class.
        /// </summary>
        /// <param name="options">The CSV options.</param>
        public CsvReader(string path, CsvOptions options) :
            base(path)
        {
            // Validate constructor parameters.
            if (options == null) { throw new System.ArgumentNullException("options"); }

            // Initialize field members.
            this._header = null;
            this._options = options;
        }
        #endregion
    }
}
