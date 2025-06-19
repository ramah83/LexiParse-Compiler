using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ScannerParserProject
{
    public class Scanner
    {
        private Dictionary<string, TokenType> keywords;
        private Dictionary<string, TokenType> operators;
        private Dictionary<string, TokenType> punctuation;

        public Scanner()
        {
            InitializeKeywords();
            InitializeOperators();
            InitializePunctuation();
        }

        private void InitializeKeywords()
        {
            keywords = new Dictionary<string, TokenType>
            {
                { "void", TokenType.VOID },
                { "real", TokenType.REAL },
                { "int", TokenType.INT },
                { "return", TokenType.RETURN },
                { "if", TokenType.IF },
                { "else", TokenType.ELSE },
                { "while", TokenType.WHILE },
                { "for", TokenType.FOR }

            };
        }

        private void InitializeOperators()
        {
            operators = new Dictionary<string, TokenType>
            {
                { "+", TokenType.PLUS },
                { "-", TokenType.MINUS },
                { "*", TokenType.TIMES },
                { "/", TokenType.DIVIDE },
                { "==", TokenType.EQUAL },
                { "!=", TokenType.NOT_EQUAL },
                { "<", TokenType.LESS_THAN },
                { ">", TokenType.GREATER_THAN },
                { "<=", TokenType.LESS_EQUAL },
                { ">=", TokenType.GREATER_EQUAL },
                { "=", TokenType.ASSIGN }
            };
        }

        private void InitializePunctuation()
        {
            punctuation = new Dictionary<string, TokenType>
            {
                { ";", TokenType.SEMICOLON },
                { ",", TokenType.COMMA },
                { "(", TokenType.LEFT_PAREN },
                { ")", TokenType.RIGHT_PAREN },
                { "[", TokenType.LEFT_BRACKET },
                { "]", TokenType.RIGHT_BRACKET },
                { "{", TokenType.LEFT_BRACE },
                { "}", TokenType.RIGHT_BRACE }
            };
        }

        public List<Token> Scan(string input)
        {
            List<Token> tokens = new List<Token>();
            int position = 0;

            while (position < input.Length)
            {
                char currentChar = input[position];

                if (char.IsWhiteSpace(currentChar))
                {
                    position++;
                    continue;
                }

                if (char.IsDigit(currentChar) || (currentChar == '-' && position + 1 < input.Length && char.IsDigit(input[position + 1])))
                {
                    string number = ScanNumber(input, ref position);
                    tokens.Add(new Token(TokenType.NUM, number, position - number.Length));
                    continue;
                }

                if (char.IsLetter(currentChar))
                {
                    string identifier = ScanIdentifier(input, ref position);
                    if (keywords.ContainsKey(identifier))
                    {
                        tokens.Add(new Token(keywords[identifier], identifier, position - identifier.Length));
                    }
                    else
                    {
                        tokens.Add(new Token(TokenType.ID, identifier, position - identifier.Length));
                    }
                    continue;
                }

                if (position + 1 < input.Length)
                {
                    string twoCharOp = input.Substring(position, 2);
                    if (operators.ContainsKey(twoCharOp))
                    {
                        tokens.Add(new Token(operators[twoCharOp], twoCharOp, position));
                        position += 2;
                        continue;
                    }
                }

                string oneCharOp = currentChar.ToString();
                if (operators.ContainsKey(oneCharOp))
                {
                    tokens.Add(new Token(operators[oneCharOp], oneCharOp, position));
                    position++;
                    continue;
                }
                else if (punctuation.ContainsKey(oneCharOp))
                {
                    tokens.Add(new Token(punctuation[oneCharOp], oneCharOp, position));
                    position++;
                    continue;
                }

                tokens.Add(new Token(TokenType.ERROR, oneCharOp, position));
                position++;
            }

            tokens.Add(new Token(TokenType.EOF, "EOF", position));
            return tokens;
        }

        private string ScanNumber(string input, ref int position)
        {
            int start = position;
            bool hasDecimal = false;

            if (input[position] == '-')
            {
                position++;
            }

            while (position < input.Length && char.IsDigit(input[position]))
            {
                position++;
            }

            if (position < input.Length && input[position] == '.')
            {
                hasDecimal = true;
                position++;

                while (position < input.Length && char.IsDigit(input[position]))
                {
                    position++;
                }
            }

            return input.Substring(start, position - start);
        }

        private string ScanIdentifier(string input, ref int position)
        {
            int start = position;

            position++;

            while (position < input.Length && (char.IsLetterOrDigit(input[position])))
            {
                position++;
            }

            return input.Substring(start, position - start);
        }
    }
}