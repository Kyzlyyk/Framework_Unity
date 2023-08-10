using System;
using System.Linq;
using System.Text;

namespace Kyzlyk.Core.Builders
{
    public sealed class LabelBuilder
    {
        public LabelBuilder(string value)
        {
            _label = value;
        }

        private string _label;

        public LabelBuilder Separate(char separator = ' ')
        {
            StringBuilder stringBuilder = new();
            
            if (_label.Length > 0)
                stringBuilder.Append(_label[0]);
            
            for (int i = 1; i < _label.Length; i++)
            {
                if (char.IsLower(_label[i - 1]) && char.IsUpper(_label[i]))
                    stringBuilder.Append(separator);
                
                stringBuilder.Append(_label[i]);
            }
            
            _label = stringBuilder.ToString();
            return this;
        }

        public LabelBuilder ToLower()
        {
            _label = _label.ToLower();
            return this;
        }

        public LabelBuilder FormatCase(string fieldName, char separator = ' ', bool upperBeforeSeparator = true, params char[] exclude)
        {
            if (string.IsNullOrEmpty(fieldName))
            {
                _label = string.Empty;
                return this;
            }

            StringBuilder stringBuilder = new();

            char previousChar = '\0';

            for (int i = 0; i < fieldName.Length; i++)
            {
                if (exclude.Contains(fieldName[i]))
                {
                    if (previousChar != '\0')
                    {
                        stringBuilder.Append(separator);
                        previousChar = separator;
                    }

                    continue;
                }

                if (upperBeforeSeparator && (previousChar == separator || previousChar == '\0'))
                    stringBuilder.Append(char.ToUpper(fieldName[i]));

                else if (char.IsLower(previousChar) && char.IsUpper(fieldName[i]))
                {
                    stringBuilder.Append(separator);
                    stringBuilder.Append(upperBeforeSeparator ? fieldName[i] : char.ToLower(fieldName[i]));
                }

                else
                    stringBuilder.Append(fieldName[i]);

                previousChar = fieldName[i];
            }

            _label = stringBuilder.ToString();

            return this;
        }

        public override string ToString() => _label;
    }
}
