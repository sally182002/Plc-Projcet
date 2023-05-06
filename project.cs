
using System;
using System.IO;
using System.Runtime.Serialization;
using com.calitha.goldparser.lalr;
using com.calitha.commons;
using System.Windows.Forms;


namespace com.calitha.goldparser
{

    [Serializable()]
    public class SymbolException : System.Exception
    {
        public SymbolException(string message) : base(message)
        {
        }

        public SymbolException(string message,
            Exception inner) : base(message, inner)
        {
        }

        protected SymbolException(SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

    }

    [Serializable()]
    public class RuleException : System.Exception
    {

        public RuleException(string message) : base(message)
        {
        }

        public RuleException(string message,
                             Exception inner) : base(message, inner)
        {
        }

        protected RuleException(SerializationInfo info,
                                StreamingContext context) : base(info, context)
        {
        }

    }

    enum SymbolConstants : int
    {
        SYMBOL_EOF          =  0, // (EOF)
        SYMBOL_ERROR        =  1, // (Error)
        SYMBOL_WHITESPACE   =  2, // Whitespace
        SYMBOL_MINUS        =  3, // '-'
        SYMBOL_MINUSMINUS   =  4, // '--'
        SYMBOL_EXCLAMEQ     =  5, // '!='
        SYMBOL_EXCLAMFALSE  =  6, // '!false'
        SYMBOL_LPAREN       =  7, // '('
        SYMBOL_RPAREN       =  8, // ')'
        SYMBOL_TIMES        =  9, // '*'
        SYMBOL_TIMESEQ      = 10, // '*='
        SYMBOL_DIV          = 11, // '/'
        SYMBOL_DIVEQ        = 12, // '/='
        SYMBOL_SEMI         = 13, // ';'
        SYMBOL_LBRACE       = 14, // '{'
        SYMBOL_RBRACE       = 15, // '}'
        SYMBOL_PLUS         = 16, // '+'
        SYMBOL_PLUSPLUS     = 17, // '++'
        SYMBOL_PLUSEQ       = 18, // '+='
        SYMBOL_LT           = 19, // '<'
        SYMBOL_LTEQ         = 20, // '<='
        SYMBOL_EQ           = 21, // '='
        SYMBOL_MINUSEQ      = 22, // '-='
        SYMBOL_GT           = 23, // '>'
        SYMBOL_GTEQ         = 24, // '>='
        SYMBOL_AUTO         = 25, // auto
        SYMBOL_ELSIF        = 26, // elsif
        SYMBOL_FLOAT        = 27, // Float
        SYMBOL_FOR          = 28, // for
        SYMBOL_FUNCTION     = 29, // function
        SYMBOL_ID           = 30, // ID
        SYMBOL_IF           = 31, // if
        SYMBOL_INTEGER      = 32, // Integer
        SYMBOL_MARE         = 33, // mare
        SYMBOL_SWITCH       = 34, // switch
        SYMBOL_TRUE         = 35, // true
        SYMBOL_VLAD         = 36, // vlad
        SYMBOL_WHILE        = 37, // while
        SYMBOL_ASSIGMENT    = 38, // <assigment>
        SYMBOL_CONTANT      = 39, // <contant>
        SYMBOL_DECLARATION  = 40, // <declaration>
        SYMBOL_EXP          = 41, // <exp>
        SYMBOL_FORAS        = 42, // <foras>
        SYMBOL_FORINCDEC    = 43, // <forincdec>
        SYMBOL_PROGRAM      = 44, // <program>
        SYMBOL_STATMENTLIST = 45  // <statmentlist>
    };

    enum RuleConstants : int
    {
        RULE_PROGRAM_VLAD_LBRACE_RBRACE_MARE                                          =  0, // <program> ::= vlad '{' <contant> '}' mare
        RULE_CONTANT                                                                  =  1, // <contant> ::= <assigment>
        RULE_CONTANT2                                                                 =  2, // <contant> ::= <declaration>
        RULE_CONTANT_FOR_LPAREN_SEMI_SEMI_RPAREN_LBRACE_RBRACE                        =  3, // <contant> ::= for '(' <foras> ';' <exp> ';' <forincdec> ')' '{' <statmentlist> '}'
        RULE_CONTANT_WHILE_LPAREN_RPAREN_LBRACE_RBRACE                                =  4, // <contant> ::= while '(' <exp> ')' '{' <statmentlist> '}'
        RULE_CONTANT_SWITCH_LPAREN_ID_RPAREN_LBRACE_RBRACE                            =  5, // <contant> ::= switch '(' ID ')' '{' <statmentlist> '}'
        RULE_CONTANT_IF_LPAREN_RPAREN_LBRACE_RBRACE                                   =  6, // <contant> ::= if '(' <exp> ')' '{' <statmentlist> '}'
        RULE_CONTANT_IF_LPAREN_RPAREN_LBRACE_RBRACE_ELSIF_LPAREN_RPAREN_LBRACE_RBRACE =  7, // <contant> ::= if '(' <exp> ')' '{' <statmentlist> '}' elsif '(' <exp> ')' '{' <statmentlist> '}'
        RULE_CONTANT_FUNCTION_LBRACE_RBRACE                                           =  8, // <contant> ::= function '{' <statmentlist> '}'
        RULE_ASSIGMENT_ID_SEMI                                                        =  9, // <assigment> ::= ID ';'
        RULE_DECLARATION_ID_EQ_INTEGER_SEMI                                           = 10, // <declaration> ::= ID '=' Integer ';'
        RULE_DECLARATION_ID_EQ_FLOAT_SEMI                                             = 11, // <declaration> ::= ID '=' Float ';'
        RULE_FORAS_AUTO_ID_EQ_INTEGER                                                 = 12, // <foras> ::= auto ID '=' Integer
        RULE_FORAS_ID_EQ_INTEGER                                                      = 13, // <foras> ::= ID '=' Integer
        RULE_FORAS_ID                                                                 = 14, // <foras> ::= ID
        RULE_FORINCDEC_ID_PLUSPLUS                                                    = 15, // <forincdec> ::= ID '++'
        RULE_FORINCDEC_ID_MINUSMINUS                                                  = 16, // <forincdec> ::= ID '--'
        RULE_FORINCDEC_PLUSPLUS_ID                                                    = 17, // <forincdec> ::= '++' ID
        RULE_FORINCDEC_MINUSMINUS_ID                                                  = 18, // <forincdec> ::= '--' ID
        RULE_FORINCDEC_ID_PLUSEQ_INTEGER                                              = 19, // <forincdec> ::= ID '+=' Integer
        RULE_FORINCDEC_ID_MINUSEQ_INTEGER                                             = 20, // <forincdec> ::= ID '-=' Integer
        RULE_FORINCDEC_ID_TIMESEQ_INTEGER                                             = 21, // <forincdec> ::= ID '*=' Integer
        RULE_FORINCDEC_ID_DIVEQ_INTEGER                                               = 22, // <forincdec> ::= ID '/=' Integer
        RULE_FORINCDEC_ID_EQ_ID_PLUS_INTEGER                                          = 23, // <forincdec> ::= ID '=' ID '+' Integer
        RULE_FORINCDEC_ID_EQ_ID_MINUS_INTEGER                                         = 24, // <forincdec> ::= ID '=' ID '-' Integer
        RULE_FORINCDEC_ID_EQ_ID_TIMES_INTEGER                                         = 25, // <forincdec> ::= ID '=' ID '*' Integer
        RULE_FORINCDEC_ID_EQ_ID_DIV_INTEGER                                           = 26, // <forincdec> ::= ID '=' ID '/' Integer
        RULE_EXP_ID_LTEQ_INTEGER                                                      = 27, // <exp> ::= ID '<=' Integer
        RULE_EXP_ID_GTEQ_INTEGER                                                      = 28, // <exp> ::= ID '>=' Integer
        RULE_EXP_ID_GT_INTEGER                                                        = 29, // <exp> ::= ID '>' Integer
        RULE_EXP_ID_LT_INTEGER                                                        = 30, // <exp> ::= ID '<' Integer
        RULE_EXP_ID_EXCLAMEQ_INTEGER                                                  = 31, // <exp> ::= ID '!=' Integer
        RULE_EXP_TRUE                                                                 = 32, // <exp> ::= true
        RULE_EXP_EXCLAMFALSE                                                          = 33, // <exp> ::= '!false'
        RULE_STATMENTLIST                                                             = 34, // <statmentlist> ::= <contant> <statmentlist>
        RULE_STATMENTLIST2                                                            = 35  // <statmentlist> ::= 
    };

    public class MyParser
    {
        private LALRParser parser;

        public MyParser(string filename, ListBox lst)
        {
            FileStream stream = new FileStream(filename,
                                               FileMode.Open, 
                                               FileAccess.Read, 
                                               FileShare.Read);
            Init(stream);
            stream.Close();
        }

        public MyParser(string baseName, string resourceName)
        {
            byte[] buffer = ResourceUtil.GetByteArrayResource(
                System.Reflection.Assembly.GetExecutingAssembly(),
                baseName,
                resourceName);
            MemoryStream stream = new MemoryStream(buffer);
            Init(stream);
            stream.Close();
        }

        public MyParser(Stream stream)
        {
            Init(stream);
        }

        private void Init(Stream stream)
        {
            CGTReader reader = new CGTReader(stream);
            parser = reader.CreateNewParser();
            parser.TrimReductions = false;
            parser.StoreTokens = LALRParser.StoreTokensMode.NoUserObject;

            parser.OnTokenError += new LALRParser.TokenErrorHandler(TokenErrorEvent);
            parser.OnParseError += new LALRParser.ParseErrorHandler(ParseErrorEvent);
        }

        public void Parse(string source)
        {
            NonterminalToken token = parser.Parse(source);
            if (token != null)
            {
                Object obj = CreateObject(token);
                //todo: Use your object any way you like
            }
        }

        private Object CreateObject(Token token)
        {
            if (token is TerminalToken)
                return CreateObjectFromTerminal((TerminalToken)token);
            else
                return CreateObjectFromNonterminal((NonterminalToken)token);
        }

        private Object CreateObjectFromTerminal(TerminalToken token)
        {
            switch (token.Symbol.Id)
            {
                case (int)SymbolConstants.SYMBOL_EOF :
                //(EOF)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ERROR :
                //(Error)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHITESPACE :
                //Whitespace
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MINUS :
                //'-'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MINUSMINUS :
                //'--'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXCLAMEQ :
                //'!='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXCLAMFALSE :
                //'!false'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LPAREN :
                //'('
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RPAREN :
                //')'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TIMES :
                //'*'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TIMESEQ :
                //'*='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIV :
                //'/'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIVEQ :
                //'/='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SEMI :
                //';'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LBRACE :
                //'{'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RBRACE :
                //'}'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLUS :
                //'+'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLUSPLUS :
                //'++'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLUSEQ :
                //'+='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LT :
                //'<'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LTEQ :
                //'<='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EQ :
                //'='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MINUSEQ :
                //'-='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GT :
                //'>'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GTEQ :
                //'>='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_AUTO :
                //auto
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ELSIF :
                //elsif
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FLOAT :
                //Float
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FOR :
                //for
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FUNCTION :
                //function
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ID :
                //ID
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IF :
                //if
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_INTEGER :
                //Integer
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MARE :
                //mare
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SWITCH :
                //switch
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TRUE :
                //true
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_VLAD :
                //vlad
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHILE :
                //while
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ASSIGMENT :
                //<assigment>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CONTANT :
                //<contant>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DECLARATION :
                //<declaration>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXP :
                //<exp>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FORAS :
                //<foras>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FORINCDEC :
                //<forincdec>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PROGRAM :
                //<program>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STATMENTLIST :
                //<statmentlist>
                //todo: Create a new object that corresponds to the symbol
                return null;

            }
            throw new SymbolException("Unknown symbol");
        }

        public Object CreateObjectFromNonterminal(NonterminalToken token)
        {
            switch (token.Rule.Id)
            {
                case (int)RuleConstants.RULE_PROGRAM_VLAD_LBRACE_RBRACE_MARE :
                //<program> ::= vlad '{' <contant> '}' mare
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONTANT :
                //<contant> ::= <assigment>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONTANT2 :
                //<contant> ::= <declaration>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONTANT_FOR_LPAREN_SEMI_SEMI_RPAREN_LBRACE_RBRACE :
                //<contant> ::= for '(' <foras> ';' <exp> ';' <forincdec> ')' '{' <statmentlist> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONTANT_WHILE_LPAREN_RPAREN_LBRACE_RBRACE :
                //<contant> ::= while '(' <exp> ')' '{' <statmentlist> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONTANT_SWITCH_LPAREN_ID_RPAREN_LBRACE_RBRACE :
                //<contant> ::= switch '(' ID ')' '{' <statmentlist> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONTANT_IF_LPAREN_RPAREN_LBRACE_RBRACE :
                //<contant> ::= if '(' <exp> ')' '{' <statmentlist> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONTANT_IF_LPAREN_RPAREN_LBRACE_RBRACE_ELSIF_LPAREN_RPAREN_LBRACE_RBRACE :
                //<contant> ::= if '(' <exp> ')' '{' <statmentlist> '}' elsif '(' <exp> ')' '{' <statmentlist> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONTANT_FUNCTION_LBRACE_RBRACE :
                //<contant> ::= function '{' <statmentlist> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ASSIGMENT_ID_SEMI :
                //<assigment> ::= ID ';'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DECLARATION_ID_EQ_INTEGER_SEMI :
                //<declaration> ::= ID '=' Integer ';'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DECLARATION_ID_EQ_FLOAT_SEMI :
                //<declaration> ::= ID '=' Float ';'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FORAS_AUTO_ID_EQ_INTEGER :
                //<foras> ::= auto ID '=' Integer
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FORAS_ID_EQ_INTEGER :
                //<foras> ::= ID '=' Integer
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FORAS_ID :
                //<foras> ::= ID
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FORINCDEC_ID_PLUSPLUS :
                //<forincdec> ::= ID '++'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FORINCDEC_ID_MINUSMINUS :
                //<forincdec> ::= ID '--'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FORINCDEC_PLUSPLUS_ID :
                //<forincdec> ::= '++' ID
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FORINCDEC_MINUSMINUS_ID :
                //<forincdec> ::= '--' ID
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FORINCDEC_ID_PLUSEQ_INTEGER :
                //<forincdec> ::= ID '+=' Integer
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FORINCDEC_ID_MINUSEQ_INTEGER :
                //<forincdec> ::= ID '-=' Integer
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FORINCDEC_ID_TIMESEQ_INTEGER :
                //<forincdec> ::= ID '*=' Integer
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FORINCDEC_ID_DIVEQ_INTEGER :
                //<forincdec> ::= ID '/=' Integer
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FORINCDEC_ID_EQ_ID_PLUS_INTEGER :
                //<forincdec> ::= ID '=' ID '+' Integer
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FORINCDEC_ID_EQ_ID_MINUS_INTEGER :
                //<forincdec> ::= ID '=' ID '-' Integer
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FORINCDEC_ID_EQ_ID_TIMES_INTEGER :
                //<forincdec> ::= ID '=' ID '*' Integer
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FORINCDEC_ID_EQ_ID_DIV_INTEGER :
                //<forincdec> ::= ID '=' ID '/' Integer
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXP_ID_LTEQ_INTEGER :
                //<exp> ::= ID '<=' Integer
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXP_ID_GTEQ_INTEGER :
                //<exp> ::= ID '>=' Integer
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXP_ID_GT_INTEGER :
                //<exp> ::= ID '>' Integer
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXP_ID_LT_INTEGER :
                //<exp> ::= ID '<' Integer
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXP_ID_EXCLAMEQ_INTEGER :
                //<exp> ::= ID '!=' Integer
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXP_TRUE :
                //<exp> ::= true
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXP_EXCLAMFALSE :
                //<exp> ::= '!false'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATMENTLIST :
                //<statmentlist> ::= <contant> <statmentlist>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATMENTLIST2 :
                //<statmentlist> ::= 
                //todo: Create a new object using the stored tokens.
                return null;

            }
            throw new RuleException("Unknown rule");
        }

        private void TokenErrorEvent(LALRParser parser, TokenErrorEventArgs args)
        {
            string message = "Token error with input: '"+args.Token.ToString()+"'";
            //todo: Report message to UI?
        }

        private void ParseErrorEvent(LALRParser parser, ParseErrorEventArgs args)
        {
            string message = "Parse error caused by token: '"+args.UnexpectedToken.ToString()+"'";
            //todo: Report message to UI?
        }

    }
}
