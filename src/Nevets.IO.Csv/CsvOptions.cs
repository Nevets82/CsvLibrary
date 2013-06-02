namespace Nevets.IO.Csv
{
    /// <summary>
    /// Represents the Comma (Character) Seperated Values options.
    /// </summary>
    public class CsvOptions
    {
        #region Field members.
        /// <summary>
        /// Contains the encoding.
        /// </summary>
        private System.Text.Encoding _encoding;

        /// <summary>
        /// Contains the has header state.
        /// </summary>
        private bool _hasHeader;

        /// <summary>
        /// Contains the value seperator character.
        /// </summary>
        private char _seperator;

        /// <summary>
        /// Contains the string character.
        /// </summary>
        private char _stringCharacter;
        #endregion

        #region Property members.
        /// <summary>
        /// Gets the encoding.
        /// </summary>
        public System.Text.Encoding Encoding
        {
            get { return this._encoding; }
        }

        /// <summary>
        /// Gets or sets the has header state.
        /// </summary>
        public bool HasHeader
        {
            get { return this._hasHeader; }
            set { this._hasHeader = value; }
        }

        /// <summary>
        /// Gets or sets the value seperator character.
        /// </summary>
        public char Seperator
        {
            get { return this._seperator; }
            set { this._seperator = value; }
        }

        /// <summary>
        /// Gets or sets the string character.
        /// </summary>
        public char StringCharacter
        {
            get { return this._stringCharacter; }
            set { this._stringCharacter = value; }
        }
        #endregion

        #region Constructor and deconstructor members.
        /// <summary>
        /// Creates a new instance of this class.
        /// </summary>
        public CsvOptions() :
            this(System.Text.Encoding.ASCII)
        {
        }

        /// <summary>
        /// Creates a new instance of this class.
        /// </summary>
        /// <param name="encoding">The encoding.</param>
        public CsvOptions(System.Text.Encoding encoding)
        {
            // Initialize field members.
            this._encoding = encoding;
            this._hasHeader = false;
            this._seperator = ',';
            this._stringCharacter = '"';
        }
        #endregion
    }
}
