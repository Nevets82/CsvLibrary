namespace Nevets.IO.Csv
{
    /// <summary>
    /// Represents a Comma (Character) Seperated Value writer.
    /// </summary>
    public class CsvWriter :
        System.IO.StreamWriter
    {
        #region Field members.
        /// <summary>
        /// Contains the CSV header.
        /// </summary>
        private string[] _header;

        /// <summary>
        /// Contains the CSV writer options.
        /// </summary>
        private CsvOptions _options;
        #endregion

        #region Property members.
        /// <summary>
        /// Gets or sets the CSV header.
        /// </summary>
        /// <remarks>The header can only be initialized once.</remarks>
        public string[] Header
        {
            get { return this._header; }
            set { this.WriteHeader(value); }
        }

        /// <summary>
        /// Gets the CSV writer options.
        /// </summary>
        public CsvOptions Options
        {
            get { return this._options; }
        }
        #endregion

        #region Public reader members.
        /// <summary>
        /// Writes the CSV header.
        /// </summary>
        /// <param name="header">The header values.</param>
        /// <remarks>The header can only be initialized once.</remarks>
        public void WriteHeader(string[] header)
        {
            if (header == null) { throw new System.ArgumentNullException("header"); }

            if (this._header != null)
            {
                throw new System.InvalidOperationException("The CSV header has already been initialized. This can be done only once.");
            }

            this._header = header;

            this.WriteRecordToStream(this._header);
        }

        /// <summary>
        /// Writes a CSV record.
        /// </summary>
        /// <param name="record">The CSV record.</param>
        public void WriteRecord(string[] record)
        {
            this.WriteRecordToStream(record);
        }

        /// <summary>
        /// Writes an array of CSV records.
        /// </summary>
        /// <param name="records">An array of CSV records.</param>
        public void WriteRecords(string[][] records)
        {
            if (records == null) { throw new System.ArgumentNullException("records"); }

            foreach (var record in records)
            {
                this.WriteRecord(record);
            }
        }
        #endregion

        #region Private reader members.
        /// <summary>
        /// Writes a CSV record to the underliing stream.
        /// </summary>
        /// <param name="record">The record.</param>
        private void WriteRecordToStream(string[] record)
        {
            var data = string.Empty;

            foreach (var value in record)
            {
                if (!string.IsNullOrEmpty(data))
                {
                    data += this.Options.Seperator;
                }

                data += this.Escape(value);
            }

            this.WriteLine(data);
        }

        /// <summary>
        /// Escapes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The escaped value.</returns>
        private string Escape(string value)
        {
            if (value == null) { value = string.Empty; }

            var seperatorCharacter = this.Options.Seperator.ToString();
            var stringCharacter = this.Options.StringCharacter.ToString();

            if (value.Contains(seperatorCharacter) || value.Contains(stringCharacter) || value.Contains("\n"))
            {
                value = value.Replace(stringCharacter, stringCharacter + stringCharacter);

                value = stringCharacter + value + stringCharacter;
            }

            return value;
        }
        #endregion

        #region Constructor and deconstructor members.
        /// <summary>
        /// Creates a new instance of this class.
        /// </summary>
        /// <param name="path">The file path to write to.</param>
        public CsvWriter(string path) :
            this(path, new CsvOptions())
        {
        }

        /// <summary>
        /// Creates a new instance of this class.
        /// </summary>
        /// <param name="path">The file path to write to.</param>
        /// <param name="append">
        /// True to append data to the file; False to overwrite the file. If the specified
        /// file does not exist, this parameter has no effect, and the constructor creates
        /// a new file.
        /// </param>
        public CsvWriter(string path, bool append) :
            this(path, append, new CsvOptions())
        {
        }

        /// <summary>
        /// Creates a new instance of this class.
        /// </summary>
        /// <param name="path">The file path to write to.</param>
        /// <param name="options">The CSV options.</param>
        public CsvWriter(string path, CsvOptions options) :
            this(path, false, options)
        {
        }

        /// <summary>
        /// Creates a new instance of this class.
        /// </summary>
        /// <param name="path">The file path to write to.</param>
        /// <param name="append">
        /// True to append data to the file; False to overwrite the file. If the specified
        /// file does not exist, this parameter has no effect, and the constructor creates
        /// a new file.
        /// </param>
        /// <param name="options">The CSV options.</param>
        public CsvWriter(string path, bool append, CsvOptions options) :
            base(path, append, options.Encoding)
        {
            // Validate constructor parameters.
            if (options == null) { throw new System.ArgumentNullException("options"); }

            // Initialize field members.
            this._header = null;
            this._options = options;
        }

        /// <summary>
        /// Creates a new instance of this class.
        /// </summary>
        /// <param name="stream">The stream to write to.</param>
        public CsvWriter(System.IO.Stream stream) :
            this(stream, new CsvOptions())
        {
        }

        /// <summary>
        /// Creates a new instance of this class.
        /// </summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="options">The CSV options.</param>
        public CsvWriter(System.IO.Stream stream, CsvOptions options) :
            base(stream, options.Encoding)
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
