using System.Text;

namespace CounterStringGenerator
{
    public class StringGenerator
    {
        public StringGenerator()
        {
            // Pre-allocate 16 MB buffer
            _counterString = new StringBuilder((1<<24) + 10);
        }

        private readonly StringBuilder _counterString;

        public string Generate(int length, char specialChar)
        {
            if (length < 0) length = 0;

            // The StringBuilder has to be cleared before appending new elements
            _counterString.Clear();

            // Allocate a little bit more memory, so that the last number fits in.
            // We'll truncate that part later
            _counterString.EnsureCapacity(length + 10);

            int lengthChange = 1+1; // Current increase of string length is 1 digit + 1 special character
            int display = 2; // First number to display is 2
            while (_counterString.Length < length)
            {
                _counterString.Append(display.ToString());
                _counterString.Append(specialChar);
                lengthChange += (display + lengthChange).ToString().Length - display.ToString().Length;
                display += lengthChange;
            }

            _counterString.Length = length;
            return _counterString.ToString();
        }
    }
}
