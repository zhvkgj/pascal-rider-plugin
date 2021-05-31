parser grammar PascalParser;

options { tokenVocab = PascalLexer; }

pascalFile
    : compoundStatement EOF
    ;
    
expression
    : simpleExpression (comparisonOp simpleExpression)?
    ; 
    
simpleExpression
    : term (additiveOp term)*
    ;
    
term
    : factor (multiplicativeOp factor)*
    ;

factor
    : LPAREN expression RPAREN
    | IDENT
    | functionCall
    | unsignedConstant
    | NOT factor
    | SIGN factor
    | setConstructor
    | valueTypecast
    | addressFactor
    ;

functionCall
    : IDENT actualParamsList
    ;

actualParamsList
    : LPAREN (expression (COMMA expression)*)? RPAREN
    ;
    
unsignedConstant
    : UnsignedNumber
    | CharacterString
    | NIL
    ;
    
setConstructor
    : LSQUARE (setGroup (COMMA setGroup)*)? RSQUARE
    ;
 
setGroup
    : expression (RANGE expression)?
    ;
    
valueTypecast  
    : IDENT LPAREN expression RPAREN
    ;
    
addressFactor
    : AT IDENT
    ;
    
comparisonOp
    : LANGLE
    | LE
    | RANGLE
    | GE
    | EQ
    | EXCL_EQ
    | IN
    | IS
    ;

additiveOp
    : ADD
    | SUB
    | OR
    | XOR
    ;
    
multiplicativeOp
    : MULT
    | DIV
    | INT_DIV
    | MOD
    | AND
    | SHL
    | SHR
    | AS 
    ;
   
statement
    : (LABEL COLON)? (simpleStatement | structuredStatement)?
    ;
    
simpleStatement
    : assigmentStatement
    | procedureStatement
    | gotoStatement
    ;
   
assigmentStatement
    : IDENT assigmentOp expression
    ;
    
assigmentOp
    : ASSIGNMENT
    | ADD_ASSIGNMENT
    | SUB_ASSIGNMENT
    | MULT_ASSIGNMENT
    | DIV_ASSIGNMENT  
    ;
    
procedureStatement
    : IDENT actualParamsList
    ;
  
gotoStatement
    : GOTO LABEL
    ;
    
structuredStatement
    : compoundStatement
    | conditionalStatement
    | repetitiveStatement
    | withStatement
    ;
    
compoundStatement
    : BEGIN statement (SEMICOLON statement)* SEMICOLON? END
    ;
    
conditionalStatement
    : caseStatement
    | ifStatement
    ;
 
caseStatement
    : CASE expression OF caseOption (SEMICOLON caseOption)* elsePart? SEMICOLON? END
    ;
    
caseOption
    : caseConstant (RANGE caseConstant)? (COMMA  caseConstant (RANGE caseConstant)?)* COLON statement
    ;
    
caseConstant
    : SignedNumber
    | CharacterString
    ;
    
elsePart
    : (ELSE | OTHERWISE) statement
    ;
    
ifStatement
    : IF expression THEN statement (ELSE statement)?
    ;

repetitiveStatement
    : forStatement
    | forInStatement
    | repeatStatement
    | whileStatement
    ;
  
forStatement
    : FOR IDENT ASSIGNMENT expression (TO | DOWNTO) expression DO statement
    ;

forInStatement
    : FOR IDENT IN expression DO statement
    ;
    
repeatStatement
    : REPEAT statement (SEMICOLON statement)* SEMICOLON? UNTIL expression
    ;
    
whileStatement
    : WHILE expression DO statement
    ;
    
withStatement
    : IDENT (COMMA IDENT)* DO statement
    ;
