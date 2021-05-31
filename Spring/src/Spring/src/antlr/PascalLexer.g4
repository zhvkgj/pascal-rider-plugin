lexer grammar PascalLexer;

// Section: delimiters
WS: [ \t\r\n] -> channel(HIDDEN);

// for case insensitive keywords
fragment A: 'a' | 'A';
fragment B: 'b' | 'B';
fragment C: 'c' | 'C';
fragment D: 'd' | 'D';
fragment E: 'e' | 'E';
fragment F: 'f' | 'F';
fragment G: 'g' | 'G';
fragment H: 'h' | 'H';
fragment I: 'i' | 'I';
fragment J: 'j' | 'J';
fragment K: 'k' | 'K';
fragment L: 'l' | 'L';
fragment M: 'm' | 'M';
fragment N: 'n' | 'N';
fragment O: 'o' | 'O';
fragment P: 'p' | 'P';
fragment Q: 'q' | 'Q';
fragment R: 'r' | 'R';
fragment S: 's' | 'S';
fragment T: 't' | 'T';
fragment U: 'u' | 'U';
fragment V: 'v' | 'V';
fragment W: 'w' | 'W';
fragment X: 'x' | 'X';
fragment Y: 'y' | 'Y';
fragment Z: 'z' | 'Z';

// Section: numbers
UnsignedNumber: UnsignedInteger | UnsignedReal;
SignedNumber: SIGN? UnsignedNumber;
fragment UnsignedInteger
    : DigitSeq 
    | DOLLAR HexDigitSeq
    | AMPERSAND OctalDigitSeq
    | PERCENT BinDigitSeq; 
fragment UnsignedReal: DigitSeq ('.' DigitSeq)? ScaleFactor?;
fragment DigitSeq: Digit+;
fragment BinDigitSeq: BinDigit+;
fragment HexDigitSeq: HexDigit+;
fragment OctalDigitSeq: OctalDigit+;

fragment HexDigit: [0-9a-fA-F];
fragment OctalDigit: '0' .. '7';
fragment BinDigit: '0' | '1';
fragment Digit: '0' .. '9';
fragment ScaleFactor: E SIGN? DigitSeq;  


// Section: comments
SINGLE_COMMENT: '//' ~[\r\n]* -> channel(HIDDEN);
MultiComment1: LPAREN MULT (DelimitedComment | . )*? MULT RPAREN -> channel(HIDDEN);
MultiComment2: LCURLY  (DelimitedComment | . )*? RCURLY -> channel(HIDDEN);
fragment DelimitedComment
    : MultiComment1
    | MultiComment2
    ;
    
DOLLAR: '$';
AMPERSAND: '&';
AT: '@';
PERCENT: '%';
SHARP: '#';
QUOTE: '\'';
COMMA: ',';
COLON: ':';
SEMICOLON: ';';
RANGE: '..';

MULT: '*';
DIV: '/';
ADD: '+';
SUB: '-';
SIGN: ADD | SUB;

// integrals ops
INT_DIV: D I V;
MOD: M O D;

// bitwise ops
XOR: X O R;
OR: O R;
AND: A N D;
SHL: S H L;
SHR: S H R;
DOUBLE_LANGLE: '<<';
DOUBLE_RANGLE: '>>';
NOT: N O T;

EXCL_EQ: '<>';
LANGLE: '<';
RANGLE: '>';
LE: '<=';
GE: '>=';
EQ: '=';

ASSIGNMENT: ':=';
ADD_ASSIGNMENT: '+=';
SUB_ASSIGNMENT: '-=';
MULT_ASSIGNMENT: '*=';
DIV_ASSIGNMENT: '/=';

IN: I N;
IS: I S;
AS: A S;
NIL: N I L;
GOTO: G O T O;
BEGIN: B E G I N;
END: E N D;
FOR: F O R;
DO: D O;
TO: T O;
DOWNTO: D O W N T O;
CASE: C A S E;
OF: O F;
IF: I F;
THEN: T H E N;
ELSE: E L S E;
OTHERWISE: O T H E R W I S E;
REPEAT: R E P E A T;
UNTIL: U N T I L;
WHILE: W H I L E;


LPAREN: '(';
RPAREN: ')';
LCURLY: '{';
RCURLY: '}';
LSQUARE: '[';
RSQUARE: ']';

// Section: identifiers
IDENT: Letter (Letter | Digit | Underscore)*;

fragment Underscore: '_';
fragment Letter: [a-zA-Z];

LABEL: DigitSeq | IDENT;

CharacterString: (QuotedString | ControlString)+;
QuotedString: QUOTE StringCharacter+ QUOTE;
StringCharacter: ~['\r\n] | '\'\'';
ControlString: (SHARP UnsignedInteger)+; 

BAD_CHARACTER: .;
