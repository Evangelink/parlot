using Moq;
using Parlot.Compilation;
using Parlot.Fluent;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;

/// <summary>
/// A minimal implementation of the Cursor class for testing purposes.
/// </summary>
public class Cursor
{
    public int Offset { get; set; } = 0;

    /// <summary>
    /// Tests that Compile returns a valid compilation result when _includeNewLines is true.
    /// Verifies that the SkipWhiteSpaceOrNewLine expression is used in the first added expression.
    /// </summary>
//     [Fact] [Error] (24-27)CS0246 The type or namespace name 'DummyCompilationContext' could not be found (are you missing a using directive or an assembly reference?)
//     public void Compile_WithIncludeNewLinesTrue_ReturnsValidCompilationResult()
//     {
//         // Arrange
//         var context = new DummyCompilationContext();
//         var parser = new WhiteSpaceLiteral(includeNewLines: true);
//         // Act
//         var result = parser.Compile(context);
//         // Assert
//         Assert.NotNull(result);
//         // Expect two expressions in the Body.
//         Assert.Equal(2, result.Body.Count);
//         // The first expression should be the one returned by SkipWhiteSpaceOrNewLine.
//         var firstExpr = result.Body[0] as ConstantExpression;
//         Assert.NotNull(firstExpr);
//         Assert.Equal("SkipWhiteSpaceOrNewLine", firstExpr.Value);
//         // The second expression should be a Block expression containing two expressions.
//         var blockExpr = result.Body[1] as BlockExpression;
//         Assert.NotNull(blockExpr);
//         Assert.Equal(2, blockExpr.Expressions.Count);
//     }

    /// <summary>
    /// Tests that Compile returns a valid compilation result when _includeNewLines is false.
    /// Verifies that the SkipWhiteSpace expression is used in the first added expression.
    /// </summary>
//     [Fact] [Error] (50-27)CS0246 The type or namespace name 'DummyCompilationContext' could not be found (are you missing a using directive or an assembly reference?)
//     public void Compile_WithIncludeNewLinesFalse_ReturnsValidCompilationResult()
//     {
//         // Arrange
//         var context = new DummyCompilationContext();
//         var parser = new WhiteSpaceLiteral(includeNewLines: false);
//         // Act
//         var result = parser.Compile(context);
//         // Assert
//         Assert.NotNull(result);
//         // Expect two expressions in the Body.
//         Assert.Equal(2, result.Body.Count);
//         // The first expression should be the one returned by SkipWhiteSpace.
//         var firstExpr = result.Body[0] as ConstantExpression;
//         Assert.NotNull(firstExpr);
//         Assert.Equal("SkipWhiteSpace", firstExpr.Value);
//         // The second expression should be a Block expression containing two expressions.
//         var blockExpr = result.Body[1] as BlockExpression;
//         Assert.NotNull(blockExpr);
//         Assert.Equal(2, blockExpr.Expressions.Count);
//     }

    /// <summary>
    /// Tests that Compile throws a NullReferenceException when a null compilation context is provided.
    /// </summary>
    [Fact]
    public void Compile_WhenContextIsNull_ThrowsNullReferenceException()
    {
        // Arrange
        var parser = new WhiteSpaceLiteral(includeNewLines: false);
        // Act & Assert
        Assert.Throws<NullReferenceException>(() => parser.Compile(null));
    }

    /// <summary>
    /// Tests that when DiscardResult is true, the second expression block contains an Expression.Empty call
    /// instead of assigning a value to the compilation result.
    /// </summary>
//     [Fact] [Error] (88-27)CS0246 The type or namespace name 'DummyCompilationContext' could not be found (are you missing a using directive or an assembly reference?) [Error] (101-37)CS0117 'ExpressionType' does not contain a definition for 'Empty'
//     public void Compile_WithDiscardResultTrue_ProducesEmptyValueAssignment()
//     {
//         // Arrange
//         var context = new DummyCompilationContext(discardResult: true);
//         var parser = new WhiteSpaceLiteral(includeNewLines: false);
//         // Act
//         var result = parser.Compile(context);
//         // Assert
//         Assert.NotNull(result);
//         // Retrieve the block expression added as the second expression.
//         var blockExpr = result.Body[1] as BlockExpression;
//         Assert.NotNull(blockExpr);
//         // The block contains two expressions.
//         Assert.Equal(2, blockExpr.Expressions.Count);
//         // The second expression should be an Expression.Empty.
//         var secondExpr = blockExpr.Expressions[1];
//         Assert.Equal(ExpressionType.Empty, secondExpr.NodeType);
//     }

    /// <summary>
    /// Tests the WhiteSpaceLiteral constructor when includeNewLines is true.
    /// This test verifies that an instance is successfully created and its Name property is set to "WhiteSpaceLiteral".
    /// </summary>
    [Fact]
    public void Constructor_WithIncludeNewLinesTrue_SetsNameProperty()
    {
        // Arrange
        bool includeNewLines = true;
        // Act
        var instance = new WhiteSpaceLiteral(includeNewLines);
        // Assert
        Assert.NotNull(instance);
        Assert.Equal("WhiteSpaceLiteral", instance.Name);
    }

    /// <summary>
    /// Tests the WhiteSpaceLiteral constructor when includeNewLines is false.
    /// This test verifies that an instance is successfully created and its Name property is set to "WhiteSpaceLiteral".
    /// </summary>
    [Fact]
    public void Constructor_WithIncludeNewLinesFalse_SetsNameProperty()
    {
        // Arrange
        bool includeNewLines = false;
        // Act
        var instance = new WhiteSpaceLiteral(includeNewLines);
        // Assert
        Assert.NotNull(instance);
        Assert.Equal("WhiteSpaceLiteral", instance.Name);
    }
}