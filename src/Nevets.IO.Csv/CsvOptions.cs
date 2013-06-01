namespace Nevets.IO.Csv
{
    /// <summary>
    /// Represents the Comma (Character) Seperated Values options.
    /// </summary>
    public class CsvOptions
    {
        #region Field members.
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
        public CsvOptions()
        {
            // Initialize field members.
            this._hasHeader = false;
            this._seperator = ',';
            this._stringCharacter = '"';
        }
        #endregion
    }
}
