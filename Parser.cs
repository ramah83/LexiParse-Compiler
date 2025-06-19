﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ScannerParserProject
{
    public class Parser
    {
        private List<Token> tokens;
        private int currentTokenIndex;
        private Token currentToken;
        private StringBuilder parseTreeBuilder;
        private int indentLevel;

        public Parser()
        {
            parseTreeBuilder = new StringBuilder();
            indentLevel = 0;
        }

        public bool Parse(List<Token> tokenList)
        {
            if (tokenList == null || tokenList.Count == 0)
            {
                parseTreeBuilder.AppendLine("Error: No tokens to parse");
                return false;
            }

            tokens = tokenList;
            currentTokenIndex = 0;
            parseTreeBuilder.Clear();
            indentLevel = 0;

            try
            {
                GetNextToken();

                parseTreeBuilder.AppendLine("Starting parse...");
                Program();
                parseTreeBuilder.AppendLine("Parse completed successfully");
                return true;
            }
            catch (Exception ex)
            {
                parseTreeBuilder.AppendLine("Parsing Error: " + ex.Message);
                return false;
            }
        }

        public string GetParseTree()
        {
            return parseTreeBuilder.ToString();
        }

        private void GetNextToken()
        {
            if (currentTokenIndex < tokens.Count)
            {
                currentToken = tokens[currentTokenIndex++];
            }
            else
            {
                currentToken = new Token(TokenType.EOF, "EOF", tokens[tokens.Count - 1].Position + 1);
            }
        }


        private void Match(TokenType expectedType)
        {
            string indent = new string(' ', indentLevel * 2);

            if (currentToken.Type == expectedType)
            {
                switch (expectedType)
                {
                    case TokenType.ID:
                        AddToParseTree("Name: " + currentToken.Lexeme);
                        break;
                    //case TokenType.INT:
                    //case TokenType.REAL:
                    //case TokenType.VOID:
                    //    parseTreeBuilder.AppendLine($"{indent}Type: {currentToken.Lexeme}");
                    //    break;
                    default:
                        parseTreeBuilder.AppendLine($"{indent}\"{currentToken.Lexeme}\"");
                        break;
                }
                GetNextToken();
            }
            else
            {
                throw new Exception($"Syntax error: Expected {expectedType}, but found {currentToken.Type} ({currentToken.Lexeme}) at position {currentToken.Position}");
            }
        }


        private void AddToParseTree(string message)
        {
            string indent = new string(' ', indentLevel * 2);
            parseTreeBuilder.AppendLine($"{indent}> {message}");
        }




        // program → declaration-list    
        private void Program()
        {
            AddToParseTree("Program");
            indentLevel++;
            DeclarationList();
            indentLevel--;
        }

        // declaration-list → declaration declaration-list | declaration
        private void DeclarationList()
        {
            AddToParseTree("Declaration List");
            indentLevel++;

            if (IsTypeSpecifier(currentToken.Type))
            {
                Declaration();

                while (IsTypeSpecifier(currentToken.Type))
                {
                    Declaration();
                }
            }
            else
            {
                throw new Exception($"Expected declaration starting with type-specifier at position {currentToken.Position}, found {currentToken.Type}");
            }

            indentLevel--;
        }
        private bool IsTypeSpecifier(TokenType type)
        {
            return type == TokenType.INT || type == TokenType.REAL || type == TokenType.VOID;
        }
        private void Declaration()
        {
            AddToParseTree("Declaration");
            indentLevel++;

            if (currentToken.Type == TokenType.INT || currentToken.Type == TokenType.REAL || currentToken.Type == TokenType.VOID)
            {
                int savedIndex = currentTokenIndex - 1;

                TypeSpecifier();
                if (currentToken.Type != TokenType.ID)
                    throw new Exception($"Expected identifier after type-specifier at position {currentToken.Position}");

                GetNextToken(); 

                if (currentToken.Type == TokenType.LEFT_PAREN)
                {
                    currentTokenIndex = savedIndex;
                    GetNextToken();
                    FunDeclaration();
                }
                else
                {
                    currentTokenIndex = savedIndex;
                    GetNextToken();
                    VarDeclaration();
                }
            }
            else
            {
                throw new Exception($"Expected type specifier at position {currentToken.Position}, found {currentToken.Type}");
            }

            indentLevel--;
        }


        // var-declaration → type-specifier ID ; | type-specifier ID[Num]; | type-specifier ID[Num] = { init - list };init-list → Num | Num , init-list
        private void InitList()
        {
            AddToParseTree("Array Initializer List");
            indentLevel++;

            Match(TokenType.NUM);

            while (currentToken.Type == TokenType.COMMA)
            {
                Match(TokenType.COMMA);
                Match(TokenType.NUM);
            }

            indentLevel--;
        }

        private void VarDeclaration()
        {
            AddToParseTree("Variable Declaration");
            indentLevel++;

            AddToParseTree("Type:");
            TypeSpecifier();

            // AddToParseTree("Name: " + currentToken.Lexeme);

            Match(TokenType.ID); 

            if (currentToken.Type == TokenType.LEFT_BRACKET)
            {
                Match(TokenType.LEFT_BRACKET);
                Match(TokenType.NUM);
                Match(TokenType.RIGHT_BRACKET);

                if (currentToken.Type == TokenType.ASSIGN)
                {
                    Match(TokenType.ASSIGN);
                    Match(TokenType.LEFT_BRACE);
                    InitList();
                    Match(TokenType.RIGHT_BRACE);
                }
            }
            else if (currentToken.Type == TokenType.ASSIGN)
            {
                Match(TokenType.ASSIGN);
                AddToParseTree("Initializer:");
                Expression();
            }

            Match(TokenType.SEMICOLON);
            indentLevel--;
        }



        // type-specifier → int | real | void
        private void TypeSpecifier()
        {
            AddToParseTree($"Type Specifier: {currentToken.Lexeme}");
            indentLevel++;

            if (currentToken.Type == TokenType.INT)
            {
                Match(TokenType.INT);
            }
            else if (currentToken.Type == TokenType.REAL)
            {
                Match(TokenType.REAL);
            }
            else if (currentToken.Type == TokenType.VOID)
            {
                Match(TokenType.VOID);
            }
            else
            {
                throw new Exception($"Expected type specifier at position {currentToken.Position}");
            }

            indentLevel--;
        }

        // fun-declaration → type-specifier ID ( params ) compound-stmt
        private void FunDeclaration()
        {
            AddToParseTree("Function Declaration");
            indentLevel++;

            TypeSpecifier();              
            AddToParseTree("Function Name:");
            Match(TokenType.ID);
            Match(TokenType.LEFT_PAREN);
            Params();
            Match(TokenType.RIGHT_PAREN);
            CompoundStmt();

            indentLevel--;
        }


        // params → param-list | void
        private void Params()
        {
            AddToParseTree("Parameters");
            indentLevel++;

            if (currentToken.Type == TokenType.VOID)
            {
                Match(TokenType.VOID);
            }
            else if (currentToken.Type == TokenType.INT || currentToken.Type == TokenType.REAL)
            {
                ParamList();
            }
            else
            {
                AddToParseTree("Empty parameter list");
            }

            indentLevel--;
        }

        public TreeNode BuildParseTree()
        {
            currentTokenIndex = 0;
            GetNextToken();

            TreeNode root = new TreeNode("Parse Tree");
            TreeNode programNode = new TreeNode("Program");
            root.Nodes.Add(programNode);

            BuildProgram(programNode);

            return root;
        }

        private void BuildProgram(TreeNode parent)
        {
            TreeNode declList = new TreeNode("Declaration List");
            parent.Nodes.Add(declList);

        }

        // param-list → param , param-list | param
        private void ParamList()
        {
            AddToParseTree("Parameter List");
            indentLevel++;

            Param();

            while (currentToken.Type == TokenType.COMMA)
            {
                Match(TokenType.COMMA);
                Param();
            }

            indentLevel--;
        }


        // param → type-specifier ID | type-specifier ID [ ]
        private void Param()
        {
            AddToParseTree("Parameter");
            indentLevel++;

            TypeSpecifier();
            Match(TokenType.ID);

            if (currentToken.Type == TokenType.LEFT_BRACKET)
            {
                Match(TokenType.LEFT_BRACKET);
                Match(TokenType.RIGHT_BRACKET);
            }

            indentLevel--;
        }

        // compound-stmt → { local-declarations stmt-list }
        private void CompoundStmt()
        {
            AddToParseTree("Compound Statement");
            indentLevel++;

            Match(TokenType.LEFT_BRACE);
            LocalDeclarations();
            StmtList();
            Match(TokenType.RIGHT_BRACE);

            indentLevel--;
        }

        // local-declarations → var-declaration local-declarations | ε
        private void LocalDeclarations()
        {
            AddToParseTree("Local Declarations");
            indentLevel++;

            if (currentToken.Type == TokenType.INT ||
                currentToken.Type == TokenType.REAL ||
                currentToken.Type == TokenType.VOID)
            {
                VarDeclaration();
                LocalDeclarations();
            }
            else
            {
                AddToParseTree("No local declarations");
            }

            indentLevel--;
        }

        // stmt-list → statement stmt-list | ε
        private void StmtList()
        {
            AddToParseTree("Statement List");
            indentLevel++;

            if (IsStatementStart(currentToken.Type))
            {
                Statement();
                StmtList();
            }
            else
            {
                AddToParseTree("No more statements");
            }

            indentLevel--;
        }

        private bool IsStatementStart(TokenType type)
        {
            return type == TokenType.ID || type == TokenType.IF ||
                   type == TokenType.WHILE || type == TokenType.RETURN ||
                   type == TokenType.LEFT_BRACE || type == TokenType.SEMICOLON || type == TokenType.FOR; 
        }

        // statement → expression-statement | compound-statement | selection-statement | iteration-statement | return-statement
        private void Statement()
        {
            AddToParseTree("Statement");
            indentLevel++;

            switch (currentToken.Type)
            {
                case TokenType.IF:
                    SelectionStatement();
                    break;
                case TokenType.WHILE:
                    IterationStatement();
                    break;
                case TokenType.FOR: // <-- أضف هذا
                    ForStatement();
                    break;
                case TokenType.RETURN:
                    ReturnStatement();
                    break;
                case TokenType.LEFT_BRACE:
                    CompoundStmt();
                    break;
                default:
                    ExpressionStatement();
                    break;
            }

            indentLevel--;
        }

        // expression-stmt → expression ; | ;
        private void ExpressionStatement()
        {
            AddToParseTree("Expression Statement");
            indentLevel++;

            if (currentToken.Type != TokenType.SEMICOLON)
            {
                Expression();
            }
            else
            {
                AddToParseTree("Empty expression");
            }

            Match(TokenType.SEMICOLON);

            indentLevel--;
        }

        // selection-statement → if ( expression ) statement | if ( expression ) statement else statement
        private void SelectionStatement()
        {
            AddToParseTree("Selection Statement (if)");
            indentLevel++;

            Match(TokenType.IF);
            Match(TokenType.LEFT_PAREN);
            Expression();
            Match(TokenType.RIGHT_PAREN);
            Statement();

            if (currentToken.Type == TokenType.ELSE)
            {
                Match(TokenType.ELSE);
                Statement();
            }

            indentLevel--;
        }

        // iteration-stmt → while ( expression ) statement
        private void IterationStatement()
        {
            AddToParseTree("Iteration Statement (while)");
            indentLevel++;

            Match(TokenType.WHILE);
            Match(TokenType.LEFT_PAREN);
            Expression();
            Match(TokenType.RIGHT_PAREN);
            Statement();

            indentLevel--;
        }

        // return-stmt → return ; | return expression ;
        private void ReturnStatement()
        {
            AddToParseTree("Return Statement");
            indentLevel++;

            Match(TokenType.RETURN);

            if (currentToken.Type != TokenType.SEMICOLON)
            {
                Expression();
            }
            else
            {
                AddToParseTree("Empty return value");
            }

            Match(TokenType.SEMICOLON);

            indentLevel--;
        }

        // expression → var = expression | simple-expression
        private void Expression()
        {
            AddToParseTree("Expression");
            indentLevel++;

            if (currentToken.Type == TokenType.ID)
            {
                // Look ahead to see if this is an assignment
                int savedIndex = currentTokenIndex - 1;
                string id = currentToken.Lexeme;
                GetNextToken();

                if (currentToken.Type == TokenType.LEFT_BRACKET)
                {

                    GetNextToken(); 
                    int bracketCount = 1;
                    while (bracketCount > 0 && currentTokenIndex < tokens.Count)
                    {
                        if (currentToken.Type == TokenType.LEFT_BRACKET) bracketCount++;
                        if (currentToken.Type == TokenType.RIGHT_BRACKET) bracketCount--;
                        GetNextToken();
                    }
                }

                if (currentToken.Type == TokenType.ASSIGN)
                {
                    currentTokenIndex = savedIndex;
                    GetNextToken();
                    AddToParseTree("Assignment Expression");
                    indentLevel++;
                    Var();
                    Match(TokenType.ASSIGN);
                    Expression();
                    indentLevel--;
                }
                else
                {
                    currentTokenIndex = savedIndex;
                    GetNextToken();
                    SimpleExpression();
                }
            }
            else
            {
                SimpleExpression();
            }

            indentLevel--;
        }

        // var → ID | ID [ expression ]
        private void Var()
        {
            AddToParseTree("Variable");
            indentLevel++;

            Match(TokenType.ID);

            if (currentToken.Type == TokenType.LEFT_BRACKET)
            {
                Match(TokenType.LEFT_BRACKET);
                Expression();
                Match(TokenType.RIGHT_BRACKET);
            }

            indentLevel--;
        }

        // simple-expression → additive-expression relOp additive-expression | additive-expression
        private void SimpleExpression()
        {
            AddToParseTree("Simple Expression");
            indentLevel++;

            AdditiveExpression();

            if (IsRelOp(currentToken.Type))
            {
                RelOp();
                AdditiveExpression();
            }

            indentLevel--;
        }

        private bool IsRelOp(TokenType type)
        {
            return type == TokenType.LESS_EQUAL || type == TokenType.LESS_THAN ||
                   type == TokenType.GREATER_EQUAL || type == TokenType.GREATER_THAN ||
                   type == TokenType.EQUAL || type == TokenType.NOT_EQUAL;
        }

        // relOp → <= | >= | < | > | == | !=
        private void RelOp()
        {
            AddToParseTree("Relational Operator");
            indentLevel++;

            if (currentToken.Type == TokenType.LESS_EQUAL)
            {
                Match(TokenType.LESS_EQUAL);
            }
            else if (currentToken.Type == TokenType.LESS_THAN)
            {
                Match(TokenType.LESS_THAN);
            }
            else if (currentToken.Type == TokenType.GREATER_EQUAL)
            {
                Match(TokenType.GREATER_EQUAL);
            }
            else if (currentToken.Type == TokenType.GREATER_THAN)
            {
                Match(TokenType.GREATER_THAN);
            }
            else if (currentToken.Type == TokenType.EQUAL)
            {
                Match(TokenType.EQUAL);
            }
            else if (currentToken.Type == TokenType.NOT_EQUAL)
            {
                Match(TokenType.NOT_EQUAL);
            }
            else
            {
                throw new Exception($"Expected relational operator at position {currentToken.Position}");
            }

            indentLevel--;
        }

        // additive-expression → term addOp additive-expression | term
        private void AdditiveExpression()
        {
            AddToParseTree("Additive Expression");
            indentLevel++;

            Term();

            while (currentToken.Type == TokenType.PLUS || currentToken.Type == TokenType.MINUS)
            {
                AddOp();
                Term();
            }

            indentLevel--;
        }


        // addOp → + | -
        private void AddOp()
        {
            AddToParseTree("Additive Operator");
            indentLevel++;

            if (currentToken.Type == TokenType.PLUS)
            {
                Match(TokenType.PLUS);
            }
            else if (currentToken.Type == TokenType.MINUS)
            {
                Match(TokenType.MINUS);
            }
            else
            {
                throw new Exception($"Expected additive operator at position {currentToken.Position}");
            }

            indentLevel--;
        }

        // term → factor mulOp term | factor
        private void Term()
        {
            AddToParseTree("Term");
            indentLevel++;

            Factor();

            if (currentToken.Type == TokenType.TIMES || currentToken.Type == TokenType.DIVIDE)
            {
                MulOp();
                Term();
            }

            indentLevel--;
        }

        // mulOp → * | /
        private void MulOp()
        {
            AddToParseTree("Multiplicative Operator");
            indentLevel++;

            if (currentToken.Type == TokenType.TIMES)
            {
                Match(TokenType.TIMES);
            }
            else if (currentToken.Type == TokenType.DIVIDE)
            {
                Match(TokenType.DIVIDE);
            }
            else
            {
                throw new Exception($"Expected multiplicative operator at position {currentToken.Position}");
            }

            indentLevel--;
        }

        // factor → ( expression ) | var | call | Num
        private void Factor()
        {
            AddToParseTree("Factor");
            indentLevel++;

            switch (currentToken.Type)
            {
                case TokenType.LEFT_PAREN:
                    Match(TokenType.LEFT_PAREN);
                    Expression();
                    Match(TokenType.RIGHT_PAREN);
                    break;
                case TokenType.ID:
                    int savedIndex = currentTokenIndex - 1;
                    string id = currentToken.Lexeme;
                    GetNextToken();

                    if (currentToken.Type == TokenType.LEFT_PAREN)
                    {
                        currentTokenIndex = savedIndex;
                        GetNextToken();
                        Call();
                    }
                    else
                    {
                        currentTokenIndex = savedIndex;
                        GetNextToken();
                        Var();
                    }
                    break;
                case TokenType.NUM:
                    Match(TokenType.NUM);
                    break;
                default:
                    throw new Exception($"Invalid factor at position {currentToken.Position}, found {currentToken.Type}");
            }

            indentLevel--;
        }

        // call → ID( args )
        private void Call()
        {
            AddToParseTree("Function Call");
            indentLevel++;

            Match(TokenType.ID);
            Match(TokenType.LEFT_PAREN);
            Args();
            Match(TokenType.RIGHT_PAREN);

            indentLevel--;
        }

        // args → args-list | ε
        private void Args()
        {
            AddToParseTree("Arguments");
            indentLevel++;

            if (currentToken.Type != TokenType.RIGHT_PAREN)
            {
                ArgsList();
            }
            else
            {
                AddToParseTree("Empty arguments");
            }

            indentLevel--;
        }

        // args-list → expression , args-list | expression
        private void ArgsList()
        {
            AddToParseTree("Arguments List");
            indentLevel++;

            Expression();  

            while (currentToken.Type == TokenType.COMMA)
            {
                Match(TokenType.COMMA);
                Expression();
            }

            indentLevel--;
        }
        private void ForStatement()
        {
            AddToParseTree("Iteration Statement (for)");
            indentLevel++;

            Match(TokenType.FOR);
            Match(TokenType.LEFT_PAREN);

            if (currentToken.Type != TokenType.SEMICOLON)
                Expression();
            Match(TokenType.SEMICOLON);

            if (currentToken.Type != TokenType.SEMICOLON)
                Expression();
            Match(TokenType.SEMICOLON);

            if (currentToken.Type != TokenType.RIGHT_PAREN)
                Expression();
            Match(TokenType.RIGHT_PAREN);

            Statement();

            indentLevel--;
        }



    }
}