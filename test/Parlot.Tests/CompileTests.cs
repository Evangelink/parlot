// using Parlot;
// using Parlot.Fluent;
// // using Parlot.Fluent.Parsers; [Error] (3-7)CS0138 A 'using namespace' directive can only be applied to namespaces; 'Parsers' is a type not a namespace. Consider a 'using static' directive instead
// using Parlot.Tests;
// using System;
// using System.Collections.Generic;
// using System.Numerics;
// using Xunit;
// 
// namespace Parlot.Tests;
// public class CompileTests
// {
// //     [Fact] [Error] (16-22)CS0103 The name 'Terms' does not exist in the current context
// //     public void ShouldCompileTextLiterals()
// //     {
// //         var parser = Terms.Text("hello").Compile();
// //         var result = parser.Parse(" hello world");
// //         Assert.NotNull(result);
// //         Assert.Equal("hello", result);
// //     }
// 
// //     [Fact] [Error] (25-22)CS0103 The name 'Terms' does not exist in the current context
// //     public void ShouldCompileStringLiterals()
// //     {
// //         var parser = Terms.String().Compile();
// //         var result = parser.Parse("'hello'");
// //         Assert.Equal("hello", result);
// //     }
// 
// //     [Fact] [Error] (33-22)CS0103 The name 'Terms' does not exist in the current context
// //     public void ShouldCompileCharLiterals()
// //     {
// //         var parser = Terms.Char('h').Compile();
// //         var result = parser.Parse(" hello world");
// //         Assert.Equal('h', result);
// //     }
// 
// //     [Fact] [Error] (41-22)CS0103 The name 'Terms' does not exist in the current context
// //     public void ShouldCompileRangeLiterals()
// //     {
// //         var parser = Terms.Pattern(static c => Character.IsInRange(c, 'a', 'z')).Compile();
// //         var result = parser.Parse("helloWorld");
// //         Assert.Equal("hello", result);
// //     }
// 
// //     [Fact] [Error] (49-22)CS0103 The name 'Terms' does not exist in the current context
// //     public void ShouldCompileDecimalLiterals()
// //     {
// //         var parser = Terms.Decimal().Compile();
// //         var result = parser.Parse(" 123");
// //         Assert.Equal(123, result);
// //     }
// 
// //     [Fact] [Error] (57-22)CS0103 The name 'Literals' does not exist in the current context
// //     public void ShouldCompileLiteralsWithoutSkipWhiteSpace()
// //     {
// //         var parser = Literals.Text("hello").Compile();
// //         var result = parser.Parse(" hello world");
// //         Assert.Null(result);
// //     }
// 
//     [Fact]
//     public void ShouldCompileCustomStringLiterals()
//     {
//         var parser = new StringLiteral('|').Compile();
//         var result = parser.Parse("|hello world|");
//         Assert.Equal("hello world", result);
//     }
// 
//     [Fact]
//     public void ShouldCompileCustomBacktickStringLiterals()
//     {
//         var parser = new StringLiteral(StringLiteralQuotes.Backtick).Compile();
//         var result = parser.Parse("`hello world`");
//         Assert.Equal("hello world", result);
//     }
// 
// //     [Fact] [Error] (81-22)CS0103 The name 'Terms' does not exist in the current context [Error] (81-45)CS0103 The name 'Terms' does not exist in the current context
// //     public void ShouldCompileOrs()
// //     {
// //         var parser = Terms.Text("hello").Or(Terms.Text("world")).Compile();
// //         var result = parser.Parse(" hello world");
// //         Assert.NotNull(result);
// //         Assert.Equal("hello", result);
// //         result = parser.Parse(" world");
// //         Assert.NotNull(result);
// //         Assert.Equal("world", result);
// //     }
// 
// //     [Fact] [Error] (93-22)CS0103 The name 'Terms' does not exist in the current context [Error] (93-46)CS0103 The name 'Terms' does not exist in the current context
// //     public void ShouldCompileAnds()
// //     {
// //         var parser = Terms.Text("hello").And(Terms.Text("world")).Compile();
// //         var result = parser.Parse(" hello world");
// //         Assert.Equal(("hello", "world"), result);
// //     }
// 
// //     [Fact] [Error] (101-22)CS0103 The name 'Terms' does not exist in the current context [Error] (101-46)CS0103 The name 'Terms' does not exist in the current context
// //     public void ShouldCompileThens()
// //     {
// //         var parser = Terms.Text("hello").And(Terms.Text("world")).Then(x => x.Item1.ToUpper()).Compile();
// //         var result = parser.Parse(" hello world");
// //         Assert.Equal("HELLO", result);
// //     }
// 
// //     [Fact] [Error] (109-24)CS1955 Non-invocable member 'Deferred<T>' cannot be used like a method. [Error] (110-27)CS0103 The name 'Terms' does not exist in the current context
// //     public void ShouldCompileDeferreds()
// //     {
// //         var deferred = Deferred<string>();
// //         deferred.Parser = Terms.Text("hello");
// //         var parser = deferred.And(deferred).Compile();
// //         var result = parser.Parse(" hello hello hello");
// //         Assert.Equal(("hello", "hello"), result);
// //     }
// 
// //     [Fact] [Error] (119-25)CS0103 The name 'Terms' does not exist in the current context [Error] (120-26)CS0103 The name 'Terms' does not exist in the current context [Error] (121-26)CS1955 Non-invocable member 'Deferred<T>' cannot be used like a method. [Error] (122-31)CS0305 Using the generic type 'Between<A, T, B>' requires 3 type arguments [Error] (123-29)CS0103 The name 'Terms' does not exist in the current context [Error] (124-22)CS0305 Using the generic type 'ZeroOrMany<T>' requires 1 type arguments
// //     public void ShouldCompileCyclicDeferred()
// //     {
// //         var openParen = Terms.Char('(');
// //         var closeParen = Terms.Char(')');
// //         var expression = Deferred<decimal>();
// //         var groupExpression = Between(openParen, expression, closeParen);
// //         expression.Parser = Terms.Decimal().Or(groupExpression);
// //         var parser = ZeroOrMany(expression).Compile();
// //         var result = parser.Parse("1 (2) 3");
// //         Assert.Equal(new decimal[] { 1, 2, 3 }, result);
// //     }
// 
// //     [Fact] [Error] (132-25)CS1955 Non-invocable member 'Deferred<T>' cannot be used like a method. [Error] (133-25)CS1955 Non-invocable member 'Deferred<T>' cannot be used like a method. [Error] (134-28)CS0103 The name 'Terms' does not exist in the current context [Error] (135-28)CS0103 The name 'Terms' does not exist in the current context
// //     public void ShouldCompileMultipleDeferred()
// //     {
// //         var deferred1 = Deferred<decimal>();
// //         var deferred2 = Deferred<decimal>();
// //         deferred1.Parser = Terms.Decimal();
// //         deferred2.Parser = Terms.Decimal();
// //         var parser = deferred1.And(deferred2).Then(x => x.Item1 + x.Item2).Compile();
// //         var result = parser.Parse("1 2");
// //         Assert.Equal(3, result);
// //     }
// 
// //     [Fact] [Error] (144-22)CS0103 The name 'Terms' does not exist in the current context [Error] (145-21)CS0103 The name 'Terms' does not exist in the current context [Error] (146-21)CS0103 The name 'Recursive' does not exist in the current context
// //     public void ShouldCompileRecursive()
// //     {
// //         var number = Terms.Decimal();
// //         var minus = Terms.Char('-');
// //         var unary = Recursive<decimal>((u) => minus.And(u).Then(static x => 0 - x.Item2).Or(number));
// //         var parser = unary.Compile();
// //         var result = parser.Parse("--1");
// //         Assert.Equal(1, result);
// //     }
// 
// //     [Fact] [Error] (155-22)CS0305 Using the generic type 'ZeroOrMany<T>' requires 1 type arguments [Error] (155-33)CS0103 The name 'Terms' does not exist in the current context [Error] (155-52)CS0103 The name 'Terms' does not exist in the current context [Error] (155-73)CS0103 The name 'Terms' does not exist in the current context
// //     public void ShouldCompileZeroOrMany()
// //     {
// //         var parser = ZeroOrMany(Terms.Text("+").Or(Terms.Text("-")).And(Terms.Integer())).Compile();
// //         Assert.Equal([], parser.Parse(""));
// //         Assert.Equal([("+", 1L)], parser.Parse("+1"));
// //         Assert.Equal([("+", 1L), ("-", 2)], parser.Parse("+1-2"));
// //         Assert.Equal([("+", 1L), ("-", 2), ("+", 3)], parser.Parse("+1-2+3"));
// //     }
// 
// //     [Fact] [Error] (165-22)CS0305 Using the generic type 'OneOrMany<T>' requires 1 type arguments [Error] (165-32)CS0103 The name 'Terms' does not exist in the current context
// //     public void ShouldCompileOneOrMany()
// //     {
// //         var parser = OneOrMany(Terms.Text("hello")).Compile();
// //         var result = parser.Parse(" hello hello hello");
// //         Assert.Equal(new[] { "hello", "hello", "hello" }, result);
// //     }
// 
// //     [Fact] [Error] (173-22)CS0305 Using the generic type 'ZeroOrOne<T>' requires 1 type arguments [Error] (173-32)CS0103 The name 'Terms' does not exist in the current context
// //     public void ShouldZeroOrOne()
// //     {
// //         var parser = ZeroOrOne(Terms.Text("hello")).Compile();
// //         Assert.Equal("hello", parser.Parse(" hello world hello"));
// //         Assert.Null(parser.Parse(" foo"));
// //     }
// 
// //     [Fact] [Error] (181-22)CS0305 Using the generic type 'ZeroOrOne<T>' requires 1 type arguments [Error] (181-32)CS0103 The name 'Terms' does not exist in the current context
// //     public void ShouldZeroOrOneWithDefault()
// //     {
// //         var parser = ZeroOrOne(Terms.Text("hello"), "world").Compile();
// //         Assert.Equal("world", parser.Parse(" this is an apple"));
// //         Assert.Equal("hello", parser.Parse(" hello world"));
// //     }
// 
// //     [Fact] [Error] (189-22)CS0305 Using the generic type 'Between<A, T, B>' requires 3 type arguments [Error] (189-30)CS0103 The name 'Terms' does not exist in the current context [Error] (189-51)CS0103 The name 'Terms' does not exist in the current context [Error] (189-72)CS0103 The name 'Terms' does not exist in the current context
// //     public void ShouldCompileBetweens()
// //     {
// //         var parser = Between(Terms.Text("hello"), Terms.Text("world"), Terms.Text("hello")).Compile();
// //         var result = parser.Parse(" hello world hello");
// //         Assert.Equal("world", result);
// //     }
// 
// //     [Fact] [Error] (197-22)CS0305 Using the generic type 'Separated<U, T>' requires 2 type arguments [Error] (197-32)CS0103 The name 'Terms' does not exist in the current context [Error] (197-49)CS0103 The name 'Terms' does not exist in the current context
// //     public void ShouldcompiledSeparated()
// //     {
// //         var parser = Separated(Terms.Char(','), Terms.Decimal()).Compile();
// //         Assert.Single(parser.Parse("1"));
// //         Assert.Equal(2, parser.Parse("1,2").Count);
// //         Assert.Null(parser.Parse(",1,"));
// //         Assert.Null(parser.Parse(""));
// //         var result = parser.Parse("1, 2,3");
// //         Assert.Equal(1, result[0]);
// //         Assert.Equal(2, result[1]);
// //         Assert.Equal(3, result[2]);
// //     }
// 
// //     [Fact] [Error] (212-22)CS0305 Using the generic type 'Separated<U, T>' requires 2 type arguments [Error] (212-32)CS0103 The name 'Terms' does not exist in the current context [Error] (212-49)CS0103 The name 'Terms' does not exist in the current context [Error] (212-74)CS0103 The name 'Terms' does not exist in the current context [Error] (212-95)CS0103 The name 'Terms' does not exist in the current context
// //     public void SeparatedShouldNotBeConsumedIfNotFollowedByValueCompiled()
// //     {
// //         // This test ensures that the separator is not consumed if there is no valid next value.
// //         var parser = Separated(Terms.Char(','), Terms.Decimal()).AndSkip(Terms.Char(',')).And(Terms.Identifier()).Then(x => true).Compile();
// //         Assert.False(parser.Parse("1"));
// //         Assert.False(parser.Parse("1,"));
// //         Assert.True(parser.Parse("1,x"));
// //     }
// 
//     [Fact]
//     public void ShouldCompileExpressionParser()
//     {
//         var parser = Calc.FluentParser.Expression.Compile();
//         var result = parser.Parse("(2 + 1) * 3");
//         Assert.Equal(9, result.Evaluate());
//     }
// 
// //     [Fact] [Error] (229-28)CS0103 The name 'Literals' does not exist in the current context [Error] (230-29)CS0103 The name 'Literals' does not exist in the current context [Error] (231-30)CS0103 The name 'Literals' does not exist in the current context [Error] (232-27)CS0103 The name 'Literals' does not exist in the current context [Error] (233-37)CS0103 The name 'Literals' does not exist in the current context [Error] (234-56)CS0305 Using the generic type 'OneOrMany<T>' requires 1 type arguments [Error] (234-66)CS0305 Using the generic type 'OneOf<A, B, T>' requires 3 type arguments [Error] (235-52)CS0305 Using the generic type 'OneOrMany<T>' requires 1 type arguments [Error] (235-62)CS0305 Using the generic type 'OneOf<A, B, T>' requires 3 type arguments [Error] (236-49)CS0305 Using the generic type 'OneOrMany<T>' requires 1 type arguments [Error] (236-59)CS0305 Using the generic type 'OneOf<A, B, T>' requires 3 type arguments [Error] (237-34)CS0305 Using the generic type 'Capture<T>' requires 1 type arguments
// //     public void ShouldCompileCapture()
// //     {
// //         Parser<char> Dot = Literals.Char('.');
// //         Parser<char> Plus = Literals.Char('+');
// //         Parser<char> Minus = Literals.Char('-');
// //         Parser<char> At = Literals.Char('@');
// //         Parser<TextSpan> WordChar = Literals.Pattern(char.IsLetterOrDigit);
// //         Parser<IReadOnlyList<char>> WordDotPlusMinus = OneOrMany(OneOf(WordChar.Then(x => 'w'), Dot, Plus, Minus));
// //         Parser<IReadOnlyList<char>> WordDotMinus = OneOrMany(OneOf(WordChar.Then(x => 'w'), Dot, Minus));
// //         Parser<IReadOnlyList<char>> WordMinus = OneOrMany(OneOf(WordChar.Then(x => 'w'), Minus));
// //         Parser<TextSpan> Email = Capture(WordDotPlusMinus.And(At).And(WordMinus).And(Dot).And(WordDotMinus));
// //         string _email = "sebastien.ros@gmail.com";
// //         var parser = Email.Compile();
// //         var result = parser.Parse(_email);
// //         Assert.Equal(_email, result.ToString());
// //     }
// 
// //     private sealed class NonCompilableCharLiteral : Parser<char> [Error] (244-26)CS0534 'CompileTests.NonCompilableCharLiteral' does not implement inherited abstract member 'Parser<char>.Parse(ParseContext, ref ParseResult<char>)'
// //     {
// //         public NonCompilableCharLiteral(char c, bool skipWhiteSpace = true)
// //         {
// //             Char = c;
// //             SkipWhiteSpace = skipWhiteSpace;
// //         }
// // 
// //         public char Char { get; }
// //         public bool SkipWhiteSpace { get; }
// // 
// //         public override bool Parse(ParseContext context, ref ParseResult<char> result) [Error] (255-30)CS0115 'CompileTests.NonCompilableCharLiteral.Parse(ParseContext, ref ParseResult<char>)': no suitable method found to override [Error] (260-25)CS1061 'ParseContext' does not contain a definition for 'SkipWhiteSpace' and no accessible extension method 'SkipWhiteSpace' accepting a first argument of type 'ParseContext' could be found (are you missing a using directive or an assembly reference?) [Error] (263-33)CS1061 'ParseContext' does not contain a definition for 'Scanner' and no accessible extension method 'Scanner' accepting a first argument of type 'ParseContext' could be found (are you missing a using directive or an assembly reference?) [Error] (264-25)CS1061 'ParseContext' does not contain a definition for 'Scanner' and no accessible extension method 'Scanner' accepting a first argument of type 'ParseContext' could be found (are you missing a using directive or an assembly reference?) [Error] (266-43)CS1061 'ParseContext' does not contain a definition for 'Scanner' and no accessible extension method 'Scanner' accepting a first argument of type 'ParseContext' could be found (are you missing a using directive or an assembly reference?)
// //         {
// //             context.EnterParser(this);
// //             if (SkipWhiteSpace)
// //             {
// //                 context.SkipWhiteSpace();
// //             }
// // 
// //             var start = context.Scanner.Cursor.Offset;
// //             if (context.Scanner.ReadChar(Char))
// //             {
// //                 result.Set(start, context.Scanner.Cursor.Offset, Char);
// //                 context.ExitParser(this);
// //                 return true;
// //             }
// // 
// //             context.ExitParser(this);
// //             return false;
// //         }
//     }
// 
//     [Fact]
//     public void ShouldCompileNonCompilableCharLiterals()
//     {
//         var parser = new NonCompilableCharLiteral('h').Compile();
//         var result = parser.Parse(" hello world");
//         Assert.Equal('h', result);
//     }
// 
// //     [Fact] [Error] (287-17)CS0103 The name 'Literals' does not exist in the current context [Error] (288-17)CS0103 The name 'Literals' does not exist in the current context
// //     public void ShouldCompileOneOfABT()
// //     {
// //         var a = Literals.Char('a');
// //         var b = Literals.Decimal();
// //         var o2 = a.Or<char, decimal, object>(b).Compile();
// //         Assert.True(o2.TryParse("a", out var c) && (char)c == 'a');
// //         Assert.True(o2.TryParse("1", out var d) && (decimal)d == 1);
// //     }
// 
// //     [Fact] [Error] (297-20)CS0103 The name 'Terms' does not exist in the current context [Error] (297-48)CS0103 The name 'Terms' does not exist in the current context
// //     public void ShouldCompileAndSkip()
// //     {
// //         var code = Terms.Text("hello").AndSkip(Terms.Integer()).Compile();
// //         Assert.False(code.TryParse("hello country", out var result));
// //         Assert.True(code.TryParse("hello 1", out result) && result == "hello");
// //     }
// 
// //     [Fact] [Error] (305-20)CS0103 The name 'Terms' does not exist in the current context [Error] (305-48)CS0103 The name 'Terms' does not exist in the current context
// //     public void ShouldCompileSkipAnd()
// //     {
// //         var code = Terms.Text("hello").SkipAnd(Terms.Integer()).Compile();
// //         Assert.False(code.TryParse("hello country", out var result));
// //         Assert.True(code.TryParse("hello 1", out result) && result == 1);
// //     }
// 
// //     [Fact] [Error] (313-21)CS0305 Using the generic type 'Always<T>' requires 1 type arguments [Error] (314-21)CS0305 Using the generic type 'Always<T>' requires 1 type arguments
// //     public void ShouldCompileEmpty()
// //     {
// //         Assert.True(Always().Compile().TryParse("123", out var result) && result == null);
// //         Assert.True(Always(1).Compile().TryParse("123", out var r2) && r2 == 1);
// //     }
// 
// //     [Fact] [Error] (320-21)CS0305 Using the generic type 'Always<T>' requires 1 type arguments [Error] (321-22)CS0305 Using the generic type 'Always<T>' requires 1 type arguments [Error] (322-21)CS0103 The name 'Terms' does not exist in the current context [Error] (323-22)CS0103 The name 'Terms' does not exist in the current context
// //     public void ShouldCompileEof()
// //     {
// //         Assert.True(Always().Eof().Compile().TryParse("", out _));
// //         Assert.False(Always().Eof().Compile().TryParse(" ", out _));
// //         Assert.True(Terms.Decimal().Eof().Compile().TryParse("123", out var result) && result == 123);
// //         Assert.False(Terms.Decimal().Eof().Compile().TryParse("123 ", out _));
// //     }
// 
// //     [Fact] [Error] (329-22)CS0305 Using the generic type 'Not<T>' requires 1 type arguments [Error] (329-26)CS0103 The name 'Terms' does not exist in the current context [Error] (330-21)CS0305 Using the generic type 'Not<T>' requires 1 type arguments [Error] (330-25)CS0103 The name 'Terms' does not exist in the current context
// //     public void ShouldCompileNot()
// //     {
// //         Assert.False(Not(Terms.Decimal()).Compile().TryParse("123", out _));
// //         Assert.True(Not(Terms.Decimal()).Compile().TryParse("Text", out _));
// //     }
// 
// //     [Fact] [Error] (336-21)CS0103 The name 'Terms' does not exist in the current context [Error] (337-21)CS0103 The name 'Terms' does not exist in the current context [Error] (338-22)CS0103 The name 'Terms' does not exist in the current context
// //     public void ShouldCompileDiscard()
// //     {
// //         Assert.True(Terms.Decimal().Discard<bool>().Compile().TryParse("123", out var r1) && r1 == false);
// //         Assert.True(Terms.Decimal().Discard<bool>(true).Compile().TryParse("123", out var r2) && r2 == true);
// //         Assert.False(Terms.Decimal().Discard<bool>(true).Compile().TryParse("abc", out _));
// //     }
// 
// //     [Fact] [Error] (344-27)CS0103 The name 'Terms' does not exist in the current context
// //     public void ShouldCompileNonWhiteSpace()
// //     {
// //         Assert.Equal("a", Terms.NonWhiteSpace(includeNewLines: true).Compile().Parse(" a"));
// //     }
// 
// //     [Fact] [Error] (350-33)CS0103 The name 'Literals' does not exist in the current context [Error] (351-28)CS0103 The name 'Literals' does not exist in the current context
// //     public void ShouldCompileWhiteSpace()
// //     {
// //         Assert.Equal("\n\r\v ", Literals.WhiteSpace(true).Compile().Parse("\n\r\v a"));
// //         Assert.Equal("  ", Literals.WhiteSpace(false).Compile().Parse("  \n\r\v a"));
// //     }
// 
// //     [Theory] [Error] (365-34)CS0103 The name 'Literals' does not exist in the current context
// //     [InlineData("a", "a")]
// //     [InlineData("foo", "foo")]
// //     [InlineData("$_", "$_")]
// //     [InlineData("a-foo.", "a")]
// //     [InlineData("abc=3", "abc")]
// //     [InlineData("abc3", "abc3")]
// //     [InlineData("abc123", "abc123")]
// //     [InlineData("abc_3", "abc_3")]
// //     public void CompiledIdentifierShouldParseValidIdentifiers(string text, string identifier)
// //     {
// //         Assert.Equal(identifier, Literals.Identifier().Compile().Parse(text).ToString());
// //     }
// 
// //     [Theory] [Error] (374-22)CS0103 The name 'Literals' does not exist in the current context
// //     [InlineData("-foo")]
// //     [InlineData("-")]
// //     [InlineData("  ")]
// //     public void CompiledIdentifierShouldNotParseInvalidIdentifiers(string text)
// //     {
// //         Assert.False(Literals.Identifier().Compile().TryParse(text, out _));
// //     }
// 
// //     [Theory] [Error] (386-28)CS0103 The name 'Literals' does not exist in the current context
// //     [InlineData("-foo")]
// //     [InlineData("/foo")]
// //     [InlineData("foo@asd")]
// //     [InlineData("foo*")]
// //     public void CompiledIdentifierShouldAcceptExtraChars(string text)
// //     {
// //         static bool start(char c) => c == '-' || c == '/';
// //         static bool part(char c) => c == '@' || c == '*';
// //         Assert.Equal(text, Literals.Identifier(start, part).Compile().Parse(text).ToString());
// //     }
// 
// //     [Fact] [Error] (392-28)CS0103 The name 'Literals' does not exist in the current context
// //     public void CompiledWhenShouldFailParserWhenFalse()
// //     {
// //         var evenIntegers = Literals.Integer().When((c, x) => x % 2 == 0).Compile();
// //         Assert.True(evenIntegers.TryParse("1234", out var result1));
// //         Assert.Equal(1234, result1);
// //         Assert.False(evenIntegers.TryParse("1235", out var result2));
// //         Assert.Equal(default, result2);
// //     }
// 
// //     [Fact] [Error] (402-28)CS0305 Using the generic type 'ZeroOrOne<T>' requires 1 type arguments [Error] (402-38)CS0103 The name 'Literals' does not exist in the current context [Error] (402-89)CS0103 The name 'Literals' does not exist in the current context
// //     public void CompiledWhenShouldResetPositionWhenFalse()
// //     {
// //         var evenIntegers = ZeroOrOne(Literals.Integer().When((c, x) => x % 2 == 0)).And(Literals.Integer()).Compile();
// //         Assert.True(evenIntegers.TryParse("1235", out var result1));
// //         Assert.Equal(1235, result1.Item2);
// //     }
// 
// //     [Fact] [Error] (411-25)CS0305 Using the generic type 'If<C, S, T>' requires 3 type arguments [Error] (411-85)CS0103 The name 'Literals' does not exist in the current context [Error] (412-24)CS0305 Using the generic type 'If<C, S, T>' requires 3 type arguments [Error] (412-84)CS0103 The name 'Literals' does not exist in the current context
// //     public void CompiledIfShouldNotInvokeParserWhenFalse()
// //     {
// //         bool invoked = false;
// //         var evenState = If(predicate: (context, x) => x % 2 == 0, state: 0, parser: Literals.Integer().Then(x => invoked = true)).Compile();
// //         var oddState = If(predicate: (context, x) => x % 2 == 0, state: 1, parser: Literals.Integer().Then(x => invoked = true)).Compile();
// //         Assert.False(oddState.TryParse("1234", out var result1));
// //         Assert.False(invoked);
// //         Assert.True(evenState.TryParse("1234", out var result2));
// //         Assert.True(invoked);
// //     }
// 
// //     [Fact] [Error] (422-22)CS0103 The name 'Literals' does not exist in the current context [Error] (424-22)CS0103 The name 'Literals' does not exist in the current context
// //     public void ErrorShouldThrowIfParserSucceeds()
// //     {
// //         Assert.False(Literals.Char('a').Error("'a' was not expected").Compile().TryParse("a", out _, out var error));
// //         Assert.Equal("'a' was not expected", error.Message);
// //         Assert.False(Literals.Char('a').Error<int>("'a' was not expected").Compile().TryParse("a", out _, out error));
// //         Assert.Equal("'a' was not expected", error.Message);
// //     }
// 
// //     [Fact] [Error] (431-22)CS0103 The name 'Literals' does not exist in the current context [Error] (433-22)CS0103 The name 'Literals' does not exist in the current context
// //     public void ErrorShouldReturnFalseThrowIfParserFails()
// //     {
// //         Assert.False(Literals.Char('a').Error("'a' was not expected").Compile().TryParse("b", out _, out var error));
// //         Assert.Null(error);
// //         Assert.False(Literals.Char('a').Error<int>("'a' was not expected").Compile().TryParse("b", out _, out error));
// //         Assert.Null(error);
// //     }
// 
// //     [Fact] [Error] (440-22)CS0103 The name 'Literals' does not exist in the current context
// //     public void ErrorShouldThrow()
// //     {
// //         Assert.False(Literals.Char('a').Error("'a' was not expected").Compile().TryParse("a", out _, out var error));
// //         Assert.Equal("'a' was not expected", error.Message);
// //     }
// 
// //     [Fact] [Error] (447-22)CS0103 The name 'Literals' does not exist in the current context
// //     public void ElseErrorShouldThrowIfParserFails()
// //     {
// //         Assert.False(Literals.Char('a').ElseError("'a' was expected").Compile().TryParse("b", out _, out var error));
// //         Assert.Equal("'a' was expected", error.Message);
// //     }
// 
// //     [Fact] [Error] (454-21)CS0103 The name 'Literals' does not exist in the current context
// //     public void ElseErrorShouldFlowResultIfParserSucceeds()
// //     {
// //         Assert.True(Literals.Char('a').ElseError("'a' was expected").Compile().TryParse("a", out var result));
// //         Assert.Equal('a', result);
// //     }
// 
// //     [Fact] [Error] (461-17)CS0103 The name 'Literals' does not exist in the current context [Error] (462-17)CS0103 The name 'Literals' does not exist in the current context [Error] (463-17)CS0103 The name 'Literals' does not exist in the current context
// //     public void ShouldCompileSwitch()
// //     {
// //         var d = Literals.Text("d:");
// //         var i = Literals.Text("i:");
// //         var s = Literals.Text("s:");
// //         var parser = d.Or(i).Or(s).Switch((context, result) =>
// //         {
// //             switch (result)
// //             {
// //                 case "d:":
// //                     return Literals.Decimal().Then<object>(x => x);
// //                 case "i:":
// //                     return Literals.Integer().Then<object>(x => x);
// //                 case "s:":
// //                     return Literals.String().Then<object>(x => x);
// //             }
// // 
// //             return null;
// //         }).Compile();
// //         Assert.True(parser.TryParse("d:123.456", out var resultD));
// //         Assert.Equal((decimal)123.456, resultD);
// //         Assert.True(parser.TryParse("i:123", out var resultI));
// //         Assert.Equal((long)123, resultI);
// //         Assert.True(parser.TryParse("s:'123'", out var resultS));
// //         Assert.Equal("123", ((TextSpan)resultS).ToString());
// //     }
// 
// //     [Fact] [Error] (489-21)CS0103 The name 'AnyCharBefore' does not exist in the current context [Error] (489-35)CS0103 The name 'Literals' does not exist in the current context [Error] (491-21)CS0103 The name 'AnyCharBefore' does not exist in the current context [Error] (491-35)CS0103 The name 'Literals' does not exist in the current context [Error] (491-59)CS0103 The name 'Literals' does not exist in the current context [Error] (492-22)CS0103 The name 'AnyCharBefore' does not exist in the current context [Error] (492-36)CS0103 The name 'Literals' does not exist in the current context [Error] (492-84)CS0103 The name 'Literals' does not exist in the current context [Error] (493-21)CS0103 The name 'AnyCharBefore' does not exist in the current context [Error] (493-35)CS0103 The name 'Literals' does not exist in the current context
// //     public void ShouldCompileTextBefore()
// //     {
// //         Assert.True(AnyCharBefore(Literals.Char('a')).Compile().TryParse("hellao", out var result1));
// //         Assert.Equal("hell", result1);
// //         Assert.True(AnyCharBefore(Literals.Char('a')).And(Literals.Char('a')).Compile().TryParse("hellao", out _));
// //         Assert.False(AnyCharBefore(Literals.Char('a'), consumeDelimiter: true).And(Literals.Char('a')).TryParse("hellao", out _));
// //         Assert.True(AnyCharBefore(Literals.Char('a')).Compile().TryParse("hella", out var result2));
// //         Assert.Equal("hell", result2);
// //     }
// 
// //     [Fact] [Error] (500-22)CS0103 The name 'Terms' does not exist in the current context [Error] (500-42)CS0103 The name 'Terms' does not exist in the current context [Error] (500-67)CS0103 The name 'Terms' does not exist in the current context [Error] (500-88)CS0103 The name 'Terms' does not exist in the current context
// //     public void ShouldCompileAndSkipWithAnd()
// //     {
// //         var parser = Terms.Char('a').And(Terms.Char('b')).AndSkip(Terms.Char('c')).And(Terms.Char('d')).Compile();
// //         Assert.True(parser.TryParse("abcd", out var result1));
// //         Assert.Equal("abd", result1.Item1.ToString() + result1.Item2 + result1.Item3);
// //     }
// 
// //     [Fact] [Error] (508-22)CS0103 The name 'Terms' does not exist in the current context [Error] (508-42)CS0103 The name 'Terms' does not exist in the current context [Error] (508-67)CS0103 The name 'Terms' does not exist in the current context [Error] (508-88)CS0103 The name 'Terms' does not exist in the current context
// //     public void ShouldCompileSkipAndWithAnd()
// //     {
// //         var parser = Terms.Char('a').And(Terms.Char('b')).SkipAnd(Terms.Char('c')).And(Terms.Char('d')).Compile();
// //         Assert.True(parser.TryParse("abcd", out var result1));
// //         Assert.Equal("acd", result1.Item1.ToString() + result1.Item2 + result1.Item3);
// //     }
// 
// //     [Fact] [Error] (516-21)CS0305 Using the generic type 'Between<A, T, B>' requires 3 type arguments [Error] (516-29)CS0103 The name 'Terms' does not exist in the current context [Error] (516-46)CS0103 The name 'Terms' does not exist in the current context [Error] (516-66)CS0103 The name 'Terms' does not exist in the current context [Error] (516-110)CS0103 The name 'Literals' does not exist in the current context
// //     public void BetweenCompiledShouldResetPosition()
// //     {
// //         Assert.True(Between(Terms.Char('['), Terms.Text("abcd"), Terms.Char(']')).Then(x => x.ToString()).Or(Literals.Text(" [abc").Compile()).TryParse(" [abc]", out var result1));
// //         Assert.Equal(" [abc", result1);
// //     }
// 
// //     [Fact] [Error] (523-20)CS0305 Using the generic type 'OneOf<A, B, T>' requires 3 type arguments [Error] (523-26)CS0103 The name 'Terms' does not exist in the current context [Error] (523-43)CS0103 The name 'Literals' does not exist in the current context
// //     public void TextWithWhiteSpaceCompiledShouldResetPosition()
// //     {
// //         var code = OneOf(Terms.Text("a"), Literals.Text(" b")).Compile();
// //         Assert.True(code.TryParse(" b", out _));
// //     }
// 
// //     [Fact] [Error] (530-22)CS0305 Using the generic type 'SkipWhiteSpace<T>' requires 1 type arguments [Error] (530-37)CS0103 The name 'Literals' does not exist in the current context
// //     public void ShouldSkipWhiteSpaceCompiled()
// //     {
// //         var parser = SkipWhiteSpace(Literals.Text("abc")).Compile();
// //         Assert.Null(parser.Parse(""));
// //         Assert.True(parser.TryParse("abc", out var result1));
// //         Assert.Equal("abc", result1);
// //         Assert.True(parser.TryParse("  abc", out var result2));
// //         Assert.Equal("abc", result2);
// //     }
// 
// //     [Fact] [Error] (541-22)CS0305 Using the generic type 'SkipWhiteSpace<T>' requires 1 type arguments [Error] (541-37)CS0103 The name 'Literals' does not exist in the current context [Error] (541-62)CS0103 The name 'Literals' does not exist in the current context
// //     public void SkipWhiteSpaceCompiledShouldResetPosition()
// //     {
// //         var parser = SkipWhiteSpace(Literals.Text("abc")).Or(Literals.Text(" ab")).Compile();
// //         Assert.True(parser.TryParse(" ab", out var result1));
// //         Assert.Equal(" ab", result1);
// //     }
// 
// //     [Fact] [Error] (550-21)CS0305 Using the generic type 'SkipWhiteSpace<T>' requires 1 type arguments [Error] (550-36)CS0103 The name 'Literals' does not exist in the current context [Error] (550-80)CS1729 'ParseContext' does not contain a constructor that takes 2 arguments [Error] (552-22)CS0305 Using the generic type 'SkipWhiteSpace<T>' requires 1 type arguments [Error] (552-37)CS0103 The name 'Literals' does not exist in the current context [Error] (552-81)CS1729 'ParseContext' does not contain a constructor that takes 2 arguments [Error] (554-21)CS0305 Using the generic type 'SkipWhiteSpace<T>' requires 1 type arguments [Error] (554-36)CS0103 The name 'Literals' does not exist in the current context [Error] (554-87)CS0103 The name 'Literals' does not exist in the current context [Error] (554-132)CS1729 'ParseContext' does not contain a constructor that takes 2 arguments
// //     public void SkipWhiteSpaceCompiledShouldResponseParseContextUseNewLines()
// //     {
// //         // Default behavior, newlines are skipped like any other space. The grammar is not "New Line Aware"
// //         Assert.True(SkipWhiteSpace(Literals.Text("ab")).Compile().TryParse(new ParseContext(new Scanner(" \nab"), useNewLines: false), out var _, out var _));
// //         // Here newlines are not skipped
// //         Assert.False(SkipWhiteSpace(Literals.Text("ab")).Compile().TryParse(new ParseContext(new Scanner(" \nab"), useNewLines: true), out var _, out var _));
// //         // Here newlines are not skipped, and the grammar reads them explicitly
// //         Assert.True(SkipWhiteSpace(Literals.WhiteSpace(includeNewLines: true).SkipAnd(Literals.Text("ab"))).Compile().TryParse(new ParseContext(new Scanner(" \nab"), useNewLines: true), out var _, out var _));
// //     }
// 
// //     [Fact] [Error] (560-22)CS0305 Using the generic type 'OneOf<A, B, T>' requires 3 type arguments [Error] (560-28)CS0103 The name 'Literals' does not exist in the current context [Error] (560-49)CS0103 The name 'Literals' does not exist in the current context [Error] (560-69)CS0103 The name 'Literals' does not exist in the current context [Error] (560-90)CS0103 The name 'Literals' does not exist in the current context
// //     public void OneOfCompileShouldNotFailWithLookupConflicts()
// //     {
// //         var parser = OneOf(Literals.Text(">="), Literals.Text(">"), Literals.Text("<="), Literals.Text("<")).Compile();
// //         Assert.Equal("<", parser.Parse("<"));
// //         Assert.Equal("<=", parser.Parse("<="));
// //         Assert.Equal(">", parser.Parse(">"));
// //         Assert.Equal(">=", parser.Parse(">="));
// //     }
// 
// //     [Fact] [Error] (570-28)CS0103 The name 'Literals' does not exist in the current context [Error] (571-29)CS0103 The name 'Literals' does not exist in the current context [Error] (572-30)CS0103 The name 'Literals' does not exist in the current context [Error] (573-27)CS0103 The name 'Literals' does not exist in the current context [Error] (574-37)CS0103 The name 'Literals' does not exist in the current context [Error] (575-56)CS0305 Using the generic type 'OneOrMany<T>' requires 1 type arguments [Error] (575-66)CS0305 Using the generic type 'OneOf<A, B, T>' requires 3 type arguments [Error] (576-52)CS0305 Using the generic type 'OneOrMany<T>' requires 1 type arguments [Error] (576-62)CS0305 Using the generic type 'OneOf<A, B, T>' requires 3 type arguments [Error] (577-49)CS0305 Using the generic type 'OneOrMany<T>' requires 1 type arguments [Error] (577-59)CS0305 Using the generic type 'OneOf<A, B, T>' requires 3 type arguments [Error] (578-34)CS0305 Using the generic type 'Capture<T>' requires 1 type arguments
// //     public void CanCompileSubTree()
// //     {
// //         Parser<char> Dot = Literals.Char('.');
// //         Parser<char> Plus = Literals.Char('+');
// //         Parser<char> Minus = Literals.Char('-');
// //         Parser<char> At = Literals.Char('@');
// //         Parser<TextSpan> WordChar = Literals.Pattern(char.IsLetterOrDigit).Compile();
// //         Parser<IReadOnlyList<char>> WordDotPlusMinus = OneOrMany(OneOf(WordChar.Then(x => 'w'), Dot, Plus, Minus));
// //         Parser<IReadOnlyList<char>> WordDotMinus = OneOrMany(OneOf(WordChar.Then(x => 'w'), Dot, Minus));
// //         Parser<IReadOnlyList<char>> WordMinus = OneOrMany(OneOf(WordChar.Then(x => 'w'), Minus));
// //         Parser<TextSpan> Email = Capture(WordDotPlusMinus.And(At).And(WordMinus).And(Dot).And(WordDotMinus));
// //         string _email = "sebastien.ros@gmail.com";
// //         var parser = Email.Compile();
// //         var result = parser.Parse(_email);
// //         Assert.Equal(_email, result.ToString());
// //     }
// 
// //     [Fact] [Error] (588-22)CS0103 The name 'Terms' does not exist in the current context [Error] (588-42)CS0103 The name 'Terms' does not exist in the current context [Error] (588-67)CS0103 The name 'Terms' does not exist in the current context [Error] (588-88)CS0103 The name 'Terms' does not exist in the current context
// //     public void ShouldSkipSequences()
// //     {
// //         var parser = Terms.Char('a').And(Terms.Char('b')).AndSkip(Terms.Char('c')).And(Terms.Char('d')).Compile();
// //         Assert.True(parser.TryParse("abcd", out var result1));
// //         Assert.Equal("abd", result1.Item1.ToString() + result1.Item2 + result1.Item3);
// //     }
// 
// //     [Fact] [Error] (596-9)CS0305 Using the generic type 'OneOf<A, B, T>' requires 3 type arguments [Error] (596-15)CS0103 The name 'Terms' does not exist in the current context [Error] (596-35)CS0103 The name 'Terms' does not exist in the current context [Error] (596-57)CS0103 The name 'Terms' does not exist in the current context
// //     public void ShouldCompileSequencesWithOneOf()
// //     {
// //         OneOf(Terms.Char('+').And(Terms.Char('-'))).And(Terms.Integer()).Compile();
// //     }
// 
// //     [Fact] [Error] (602-17)CS0103 The name 'Literals' does not exist in the current context [Error] (603-17)CS0103 The name 'Literals' does not exist in the current context [Error] (604-17)CS0103 The name 'Literals' does not exist in the current context [Error] (605-17)CS0103 The name 'Literals' does not exist in the current context [Error] (606-17)CS0103 The name 'Literals' does not exist in the current context [Error] (607-17)CS0103 The name 'Literals' does not exist in the current context [Error] (608-17)CS0103 The name 'Literals' does not exist in the current context [Error] (609-17)CS0103 The name 'Literals' does not exist in the current context
// //     public void ShouldParseSequenceCompile()
// //     {
// //         var a = Literals.Char('a');
// //         var b = Literals.Char('b');
// //         var c = Literals.Char('c');
// //         var d = Literals.Char('d');
// //         var e = Literals.Char('e');
// //         var f = Literals.Char('f');
// //         var g = Literals.Char('g');
// //         var h = Literals.Char('h');
// //         Assert.True(a.And(b).Compile().TryParse("ab", out var r));
// //         Assert.Equal(('a', 'b'), r);
// //         Assert.True(a.And(b).And(c).Compile().TryParse("abc", out var r1));
// //         Assert.Equal(('a', 'b', 'c'), r1);
// //         Assert.True(a.And(b).AndSkip(c).Compile().TryParse("abc", out var r2));
// //         Assert.Equal(('a', 'b'), r2);
// //         Assert.True(a.And(b).SkipAnd(c).Compile().TryParse("abc", out var r3));
// //         Assert.Equal(('a', 'c'), r3);
// //     }
// 
// //     [Fact] [Error] (623-17)CS0103 The name 'Literals' does not exist in the current context [Error] (624-17)CS0103 The name 'Literals' does not exist in the current context [Error] (625-17)CS0103 The name 'Literals' does not exist in the current context [Error] (626-17)CS0103 The name 'Literals' does not exist in the current context [Error] (627-17)CS0103 The name 'Literals' does not exist in the current context [Error] (628-17)CS0103 The name 'Literals' does not exist in the current context [Error] (629-17)CS0103 The name 'Literals' does not exist in the current context [Error] (630-17)CS0103 The name 'Literals' does not exist in the current context
// //     public void ShouldParseSequenceAndSkipCompile()
// //     {
// //         var a = Literals.Char('a');
// //         var b = Literals.Char('b');
// //         var c = Literals.Char('c');
// //         var d = Literals.Char('d');
// //         var e = Literals.Char('e');
// //         var f = Literals.Char('f');
// //         var g = Literals.Char('g');
// //         var h = Literals.Char('h');
// //         Assert.True(a.AndSkip(b).Compile().TryParse("ab", out var r));
// //         Assert.Equal(('a'), r);
// //         Assert.True(a.AndSkip(b).And(c).Compile().TryParse("abc", out var r1));
// //         Assert.Equal(('a', 'c'), r1);
// //         Assert.True(a.AndSkip(b).AndSkip(c).Compile().TryParse("abc", out var r2));
// //         Assert.Equal(('a'), r2);
// //         Assert.True(a.AndSkip(b).SkipAnd(c).Compile().TryParse("abc", out var r3));
// //         Assert.Equal(('c'), r3);
// //     }
// 
// //     [Fact] [Error] (644-17)CS0103 The name 'Literals' does not exist in the current context [Error] (645-17)CS0103 The name 'Literals' does not exist in the current context [Error] (646-17)CS0103 The name 'Literals' does not exist in the current context [Error] (647-17)CS0103 The name 'Literals' does not exist in the current context [Error] (648-17)CS0103 The name 'Literals' does not exist in the current context [Error] (649-17)CS0103 The name 'Literals' does not exist in the current context [Error] (650-17)CS0103 The name 'Literals' does not exist in the current context [Error] (651-17)CS0103 The name 'Literals' does not exist in the current context
// //     public void ShouldParseSequenceSkipAndCompile()
// //     {
// //         var a = Literals.Char('a');
// //         var b = Literals.Char('b');
// //         var c = Literals.Char('c');
// //         var d = Literals.Char('d');
// //         var e = Literals.Char('e');
// //         var f = Literals.Char('f');
// //         var g = Literals.Char('g');
// //         var h = Literals.Char('h');
// //         Assert.True(a.SkipAnd(b).Compile().TryParse("ab", out var r));
// //         Assert.Equal(('b'), r);
// //         Assert.True(a.SkipAnd(b).And(c).Compile().TryParse("abc", out var r1));
// //         Assert.Equal(('b', 'c'), r1);
// //         Assert.True(a.SkipAnd(b).AndSkip(c).Compile().TryParse("abc", out var r2));
// //         Assert.Equal(('b'), r2);
// //         Assert.True(a.SkipAnd(b).SkipAnd(c).Compile().TryParse("abc", out var r3));
// //         Assert.Equal(('c'), r3);
// //     }
// 
// //     [Fact] [Error] (665-17)CS0103 The name 'Literals' does not exist in the current context [Error] (666-17)CS0103 The name 'Literals' does not exist in the current context
// //     public void ShouldReturnConstantResult()
// //     {
// //         var a = Literals.Char('a').Then(123).Compile();
// //         var b = Literals.Char('b').Then("1").Compile();
// //         Assert.Equal(123, a.Parse("a"));
// //         Assert.Equal("1", b.Parse("b"));
// //     }
// 
// //     [Fact] [Error] (674-22)CS0305 Using the generic type 'ZeroOrMany<T>' requires 1 type arguments [Error] (674-33)CS0103 The name 'Terms' does not exist in the current context [Error] (674-52)CS0103 The name 'Terms' does not exist in the current context [Error] (674-73)CS0103 The name 'Terms' does not exist in the current context
// //     public void ZeroOrManyShouldHandleAllSizes()
// //     {
// //         var parser = ZeroOrMany(Terms.Text("+").Or(Terms.Text("-")).And(Terms.Integer())).Compile();
// //         Assert.Equal([], parser.Parse(""));
// //         Assert.Equal([("+", 1L)], parser.Parse("+1"));
// //         Assert.Equal([("+", 1L), ("-", 2)], parser.Parse("+1-2"));
// //     }
// 
// //     [Fact] [Error] (683-23)CS0103 The name 'Literals' does not exist in the current context [Error] (687-23)CS0103 The name 'Terms' does not exist in the current context
// //     public void ShouldParseWithCaseSensitivity()
// //     {
// //         var parser1 = Literals.Text("not", caseInsensitive: true).Compile();
// //         Assert.Equal("not", parser1.Parse("not"));
// //         Assert.Equal("not", parser1.Parse("nOt"));
// //         Assert.Equal("not", parser1.Parse("NOT"));
// //         var parser2 = Terms.Text("not", caseInsensitive: true).Compile();
// //         Assert.Equal("not", parser2.Parse("not"));
// //         Assert.Equal("not", parser2.Parse("nOt"));
// //         Assert.Equal("not", parser2.Parse("NOT"));
// //     }
// 
// //     [Fact] [Error] (696-22)CS0305 Using the generic type 'OneOf<A, B, T>' requires 3 type arguments [Error] (696-28)CS0103 The name 'Literals' does not exist in the current context [Error] (696-73)CS0103 The name 'Literals' does not exist in the current context [Error] (696-119)CS0103 The name 'Literals' does not exist in the current context
// //     public void ShouldBuildCaseInsensitiveLookupTable()
// //     {
// //         var parser = OneOf(Literals.Text("not", caseInsensitive: true), Literals.Text("abc", caseInsensitive: false), Literals.Text("aBC", caseInsensitive: false)).Compile();
// //         Assert.Equal("not", parser.Parse("not"));
// //         Assert.Equal("not", parser.Parse("nOt"));
// //         Assert.Equal("abc", parser.Parse("abc"));
// //         Assert.Equal("aBC", parser.Parse("aBC"));
// //         Assert.Null(parser.Parse("ABC"));
// //     }
// 
// //     [Fact] [Error] (707-22)CS0103 The name 'Literals' does not exist in the current context
// //     public void ShouldReturnElse()
// //     {
// //         var parser = Literals.Integer().Then<long?>(x => x).Else(null).Compile();
// //         Assert.True(parser.TryParse("123", out var result1));
// //         Assert.Equal(123, result1);
// //         Assert.True(parser.TryParse(" 123", out var result2));
// //         Assert.Null(result2);
// //     }
// 
// //     [Fact] [Error] (717-22)CS0103 The name 'Literals' does not exist in the current context
// //     public void ShouldThenElse()
// //     {
// //         var parser = Literals.Integer().ThenElse<long?>(x => x, null).Compile();
// //         Assert.True(parser.TryParse("123", out var result1));
// //         Assert.Equal(123, result1);
// //         Assert.True(parser.TryParse(" 123", out var result2));
// //         Assert.Null(result2);
// //     }
// 
//     private class LogicalExpression
//     {
//     }
// 
//     private class ValueExpression(decimal Value) : LogicalExpression
//     {
//         public decimal Value { get; } = Value;
//     }
// 
// //     [Fact] [Error] (736-21)CS0103 The name 'Terms' does not exist in the current context [Error] (737-21)CS0103 The name 'Terms' does not exist in the current context
// //     public void IntegersShouldAcceptSignByDefault()
// //     {
// //         Assert.True(Terms.Integer().Compile().TryParse("-123", out _));
// //         Assert.True(Terms.Integer().Compile().TryParse("+123", out _));
// //     }
// 
// //     [Fact] [Error] (743-21)CS0103 The name 'Terms' does not exist in the current context [Error] (744-21)CS0103 The name 'Terms' does not exist in the current context
// //     public void DecimalsShouldAcceptSignByDefault()
// //     {
// //         Assert.True(Terms.Decimal().Compile().TryParse("-123", out _));
// //         Assert.True(Terms.Decimal().Compile().TryParse("+123", out _));
// //     }
// 
// //     [Fact] [Error] (750-28)CS0103 The name 'Terms' does not exist in the current context [Error] (751-28)CS0103 The name 'Terms' does not exist in the current context [Error] (752-27)CS0103 The name 'Terms' does not exist in the current context [Error] (753-27)CS0103 The name 'Terms' does not exist in the current context
// //     public void NumbersShouldAcceptSignIfAllowed()
// //     {
// //         Assert.Equal(-123, Terms.Decimal(NumberOptions.AllowLeadingSign).Compile().Parse("-123"));
// //         Assert.Equal(-123, Terms.Integer(NumberOptions.AllowLeadingSign).Compile().Parse("-123"));
// //         Assert.Equal(123, Terms.Decimal(NumberOptions.AllowLeadingSign).Compile().Parse("+123"));
// //         Assert.Equal(123, Terms.Integer(NumberOptions.AllowLeadingSign).Compile().Parse("+123"));
// //     }
// 
// //     [Fact] [Error] (759-22)CS0103 The name 'Terms' does not exist in the current context [Error] (760-22)CS0103 The name 'Terms' does not exist in the current context [Error] (761-22)CS0103 The name 'Terms' does not exist in the current context [Error] (762-22)CS0103 The name 'Terms' does not exist in the current context
// //     public void NumbersShouldNotAcceptSignIfNotAllowed()
// //     {
// //         Assert.False(Terms.Decimal(NumberOptions.None).Compile().TryParse("-123", out _));
// //         Assert.False(Terms.Integer(NumberOptions.None).Compile().TryParse("-123", out _));
// //         Assert.False(Terms.Decimal(NumberOptions.None).Compile().TryParse("+123", out _));
// //         Assert.False(Terms.Integer(NumberOptions.None).Compile().TryParse("+123", out _));
// //     }
// 
// //     [Fact] [Error] (768-33)CS0103 The name 'Literals' does not exist in the current context [Error] (769-34)CS0103 The name 'Literals' does not exist in the current context [Error] (770-32)CS0103 The name 'Literals' does not exist in the current context [Error] (771-33)CS0103 The name 'Literals' does not exist in the current context [Error] (772-33)CS0103 The name 'Literals' does not exist in the current context [Error] (773-34)CS0103 The name 'Literals' does not exist in the current context [Error] (774-34)CS0103 The name 'Literals' does not exist in the current context [Error] (775-35)CS0103 The name 'Literals' does not exist in the current context [Error] (776-36)CS0103 The name 'Literals' does not exist in the current context [Error] (777-35)CS0103 The name 'Literals' does not exist in the current context [Error] (778-34)CS0103 The name 'Literals' does not exist in the current context [Error] (779-33)CS0103 The name 'Literals' does not exist in the current context [Error] (780-39)CS0103 The name 'Literals' does not exist in the current context
// //     public void NumberReturnsAnyType()
// //     {
// //         Assert.Equal((byte)123, Literals.Number<byte>().Compile().Parse("123"));
// //         Assert.Equal((sbyte)123, Literals.Number<sbyte>().Compile().Parse("123"));
// //         Assert.Equal((int)123, Literals.Number<int>().Compile().Parse("123"));
// //         Assert.Equal((uint)123, Literals.Number<uint>().Compile().Parse("123"));
// //         Assert.Equal((long)123, Literals.Number<long>().Compile().Parse("123"));
// //         Assert.Equal((ulong)123, Literals.Number<ulong>().Compile().Parse("123"));
// //         Assert.Equal((short)123, Literals.Number<short>().Compile().Parse("123"));
// //         Assert.Equal((ushort)123, Literals.Number<ushort>().Compile().Parse("123"));
// //         Assert.Equal((decimal)123, Literals.Number<decimal>().Compile().Parse("123"));
// //         Assert.Equal((double)123, Literals.Number<double>().Compile().Parse("123"));
// //         Assert.Equal((float)123, Literals.Number<float>().Compile().Parse("123"));
// //         Assert.Equal((Half)123, Literals.Number<Half>().Compile().Parse("123"));
// //         Assert.Equal((BigInteger)123, Literals.Number<BigInteger>().Compile().Parse("123"));
// // #if NET8_0_OR_GREATER
// //         Assert.Equal((nint)123, Literals.Number<nint>().Compile().Parse("123"));
// //         Assert.Equal((nuint)123, Literals.Number<nuint>().Compile().Parse("123"));
// //         Assert.Equal((Int128)123, Literals.Number<Int128>().Compile().Parse("123"));
// //         Assert.Equal((UInt128)123, Literals.Number<UInt128>().Compile().Parse("123"));
// // #endif
// //     }
// 
// //     [Fact] [Error] (793-33)CS0103 The name 'Literals' does not exist in the current context [Error] (794-34)CS0103 The name 'Literals' does not exist in the current context [Error] (795-32)CS0103 The name 'Literals' does not exist in the current context [Error] (796-33)CS0103 The name 'Literals' does not exist in the current context [Error] (797-33)CS0103 The name 'Literals' does not exist in the current context [Error] (798-34)CS0103 The name 'Literals' does not exist in the current context [Error] (799-34)CS0103 The name 'Literals' does not exist in the current context [Error] (800-35)CS0103 The name 'Literals' does not exist in the current context [Error] (801-36)CS0103 The name 'Literals' does not exist in the current context [Error] (802-35)CS0103 The name 'Literals' does not exist in the current context [Error] (803-34)CS0103 The name 'Literals' does not exist in the current context [Error] (804-33)CS0103 The name 'Literals' does not exist in the current context [Error] (805-39)CS0103 The name 'Literals' does not exist in the current context
// //     public void NumberCanReadExponent()
// //     {
// //         var e = NumberOptions.AllowExponent;
// //         Assert.Equal((byte)120, Literals.Number<byte>(e).Compile().Parse("12e1"));
// //         Assert.Equal((sbyte)120, Literals.Number<sbyte>(e).Compile().Parse("12e1"));
// //         Assert.Equal((int)120, Literals.Number<int>(e).Compile().Parse("12e1"));
// //         Assert.Equal((uint)120, Literals.Number<uint>(e).Compile().Parse("12e1"));
// //         Assert.Equal((long)120, Literals.Number<long>(e).Compile().Parse("12e1"));
// //         Assert.Equal((ulong)120, Literals.Number<ulong>(e).Compile().Parse("12e1"));
// //         Assert.Equal((short)120, Literals.Number<short>(e).Compile().Parse("12e1"));
// //         Assert.Equal((ushort)120, Literals.Number<ushort>(e).Compile().Parse("12e1"));
// //         Assert.Equal((decimal)120, Literals.Number<decimal>(e).Compile().Parse("12e1"));
// //         Assert.Equal((double)120, Literals.Number<double>(e).Compile().Parse("12e1"));
// //         Assert.Equal((float)120, Literals.Number<float>(e).Compile().Parse("12e1"));
// //         Assert.Equal((Half)120, Literals.Number<Half>(e).Compile().Parse("12e1"));
// //         Assert.Equal((BigInteger)120, Literals.Number<BigInteger>(e).Compile().Parse("12e1"));
// // #if NET8_0_OR_GREATER
// //         Assert.Equal((nint)120, Literals.Number<nint>(e).Compile().Parse("12e1"));
// //         Assert.Equal((nuint)120, Literals.Number<nuint>(e).Compile().Parse("12e1"));
// //         Assert.Equal((Int128)120, Literals.Number<Int128>(e).Compile().Parse("12e1"));
// //         Assert.Equal((UInt128)120, Literals.Number<UInt128>(e).Compile().Parse("12e1"));
// // #endif
// //     }
// 
// //     [Theory] [Error] (842-32)CS0103 The name 'Literals' does not exist in the current context
// //     [InlineData(1, "1")]
// //     [InlineData(1, "+1")]
// //     [InlineData(-1, "-1")]
// //     [InlineData(1, "1.0")]
// //     [InlineData(1, "1.00")]
// //     [InlineData(.1, ".1")]
// //     [InlineData(1.1, "1.1")]
// //     [InlineData(1.123, "1.123")]
// //     [InlineData(1.123, "+1.123")]
// //     [InlineData(-1.123, "-1.123")]
// //     [InlineData(1123, "1,123")]
// //     [InlineData(1123, "1,1,,2,3")]
// //     [InlineData(1123, "+1,123")]
// //     [InlineData(-1123, "-1,1,,2,3")]
// //     [InlineData(1123.123, "1,123.123")]
// //     [InlineData(1123.123, "1,1,,2,3.123")]
// //     [InlineData(10, "1e1")]
// //     [InlineData(11, "1.1e1")]
// //     [InlineData(1, ".1e1")]
// //     [InlineData(10, "1e+1")]
// //     [InlineData(11, "1.1e+1")]
// //     [InlineData(1, ".1e+1")]
// //     [InlineData(0.1, "1e-1")]
// //     [InlineData(0.11, "1.1e-1")]
// //     [InlineData(0.01, ".1e-1")]
// //     public void NumberParsesAllNumbers(decimal expected, string source)
// //     {
// //         Assert.Equal(expected, Literals.Number<decimal>(NumberOptions.Any).Compile().Parse(source));
// //     }
// 
// //     [Fact] [Error] (848-40)CS0103 The name 'Literals' does not exist in the current context
// //     public void NumberParsesCustomDecimalSeparator()
// //     {
// //         Assert.Equal((decimal)123.456, Literals.Number<decimal>(NumberOptions.Any, decimalSeparator: '|').Compile().Parse("123|456"));
// //     }
// 
// //     [Fact] [Error] (854-39)CS0103 The name 'Literals' does not exist in the current context
// //     public void NumberParsesCustomGroupSeparator()
// //     {
// //         Assert.Equal((decimal)123456, Literals.Number<decimal>(NumberOptions.Any, groupSeparator: '|').Compile().Parse("123|456"));
// //     }
// 
// //     [Theory] [Error] (863-22)CS0305 Using the generic type 'ZeroOrMany<T>' requires 1 type arguments [Error] (863-33)CS0103 The name 'Literals' does not exist in the current context
// //     [InlineData("")]
// //     [InlineData("+")]
// //     [InlineData("+++")]
// //     public void ZeroOrManyShouldSucceed(string source)
// //     {
// //         var parser = ZeroOrMany(Literals.Char('+')).Compile();
// //         Assert.True(parser.TryParse(source, out var result));
// //         Assert.Equal(source.Length, result.Count);
// //     }
// }
