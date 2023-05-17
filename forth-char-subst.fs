\ single-character removal procedure for pforth
\ by gustav melck, may 2023

private{

: tail-recursive  ( l? -- )  postpone if  postpone r> postpone drop  postpone then  ;  immediate
: tail-recurse  ( -- )  postpone true postpone recurse  ;  immediate

create *remchar 0 c,

: (remove-char)  ( addr u addr' l? -- addr' )  tail-recursive
    >r
    ?dup 0=  if  drop r>  else
        over c@ dup *remchar c@ <>  if
            r@ c!
            1- swap 1+ swap
            r> 1+
            tail-recurse
        else
            drop
            1- swap 1+ swap
            r>
            tail-recurse
        then
    then  ;

}private

: remove-char  ( addr u char -- addr u' )  \ destructively remove a character from a string, from left to right
    *remchar c!
    over dup >r
    false (remove-char)
    r@ - r> swap  ;

privatize

\ test
\ 
\ s" 123,456,789.54" 2dup type ." ;" cr
\ .s
\ char , remove-char
\ .s
\ type ." ;" cr
\ .s

