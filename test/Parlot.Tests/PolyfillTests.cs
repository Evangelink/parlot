using Parlot;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

/// <summary>
/// Unit tests for the <see cref = "Polyfill"/> class.
/// </summary>
public class PolyfillTests
{
    /// <summary>
    /// Tests that Append correctly appends an element to a non-empty collection.
    /// </summary>
    [Fact]
    public void Append_WithNonEmptySource_AppendsElementAtEnd()
    {
        // Arrange
        IEnumerable<int> source = new List<int>
        {
            1,
            2,
            3
        };
        int elementToAppend = 4;
        var expected = new List<int>
        {
            1,
            2,
            3,
            4
        };
        // Act
        var result = source.Append(elementToAppend);
        // Assert
        Assert.NotNull(result);
        Assert.Equal(expected, result.ToList());
    }

    /// <summary>
    /// Tests that Append correctly appends an element to an empty collection.
    /// </summary>
    [Fact]
    public void Append_WithEmptySource_AppendsElement()
    {
        // Arrange
        IEnumerable<string> source = new List<string>();
        string elementToAppend = "test";
        var expected = new List<string>
        {
            "test"
        };
        // Act
        var result = source.Append(elementToAppend);
        // Assert
        Assert.NotNull(result);
        Assert.Equal(expected, result.ToList());
    }

    /// <summary>
    /// Tests that Append throws an ArgumentNullException when the source collection is null.
    /// </summary>
    [Fact]
    public void Append_WithNullSource_ThrowsArgumentNullException()
    {
        // Arrange
        IEnumerable<double> source = null;
        double elementToAppend = 3.14;
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => source.Append(elementToAppend));
    }

    /// <summary>
    /// Tests that Append correctly appends a null element into a collection of reference types.
    /// </summary>
    [Fact]
    public void Append_WithNullElement_AppendsNullElementAtEnd()
    {
        // Arrange
        IEnumerable<string> source = new List<string>
        {
            "alpha",
            "beta"
        };
        string elementToAppend = null;
        var expected = new List<string>
        {
            "alpha",
            "beta",
            null
        };
        // Act
        var result = source.Append(elementToAppend);
        // Assert
        Assert.NotNull(result);
        Assert.Equal(expected, result.ToList());
    }

    /// <summary>
    /// Tests the <see cref = "Polyfill.Create{TState}(string, int, TState, Polyfill.SpanAction{char, TState})"/> method with valid input,
    /// verifying that it correctly invokes the span action and returns the expected resultant string.
    /// </summary>
//     [Fact] [Error] (110-9)CS0122 'Polyfill' is inaccessible due to its protection level
//     public void Create_WithValidInput_ReturnsExpectedString()
//     {
//         // Arrange
//         string dummy = "dummy";
//         int length = 5;
//         string expected = "Hello";
//         Polyfill.SpanAction<char, string> action = (span, state) =>
//         {
//             for (int i = 0; i < span.Length && i < state.Length; i++)
//             {
//                 span[i] = state[i];
//             }
//         };
//         // Act
//         string result = dummy.Create(length, expected, action);
//         // Assert
//         Assert.Equal(expected, result);
//     }

    /// <summary>
    /// Tests the <see cref = "Polyfill.Create{TState}(string, int, TState, Polyfill.SpanAction{char, TState})"/> method 
    /// when a zero length is provided, ensuring it returns an empty string.
    /// </summary>
//     [Fact] [Error] (134-9)CS0122 'Polyfill' is inaccessible due to its protection level
//     public void Create_WithZeroLength_ReturnsEmptyString()
//     {
//         // Arrange
//         string dummy = "dummy";
//         int length = 0;
//         string state = "ignored"; // state value is irrelevant for zero length.
//         Polyfill.SpanAction<char, string> action = (span, arg) =>
//         {
//             // Since span length is zero, this action is not expected to be invoked.
//             Assert.Empty(span);
//         };
//         // Act
//         string result = dummy.Create(length, state, action);
//         // Assert
//         Assert.Equal(string.Empty, result);
//     }

    /// <summary>
    /// Tests the <see cref = "Polyfill.Create{TState}(string, int, TState, Polyfill.SpanAction{char, TState})"/> method 
    /// with a null span action to ensure it throws a <see cref = "NullReferenceException"/>.
    /// </summary>
//     [Fact] [Error] (157-53)CS0176 Member 'string.Create<string>(int, string, SpanAction<char, string>)' cannot be accessed with an instance reference; qualify it with a type name instead
//     public void Create_WithNullAction_ThrowsNullReferenceException()
//     {
//         // Arrange
//         string dummy = "dummy";
//         int length = 5;
//         string state = "Hello";
//         // Act & Assert
//         Assert.Throws<NullReferenceException>(() => dummy.Create(length, state, null));
//     }

    /// <summary>
    /// Tests the <see cref = "Polyfill.Create{TState}(string, int, TState, Polyfill.SpanAction{char, TState})"/> method 
    /// with a negative length to ensure it throws an <see cref = "OverflowException"/>.
    /// </summary>
//     [Fact] [Error] (171-9)CS0122 'Polyfill' is inaccessible due to its protection level [Error] (175-9)CS0619 'Assert.Throws<T>(Func<Task>)' is obsolete: 'You must call Assert.ThrowsAsync<T> (and await the result) when testing async code.'
//     public void Create_WithNegativeLength_ThrowsOverflowException()
//     {
//         // Arrange
//         string dummy = "dummy";
//         int length = -1;
//         string state = "Hello";
//         Polyfill.SpanAction<char, string> action = (span, arg) =>
//         { /* No operation as exception is expected before invocation. */
//         };
//         // Act & Assert
//         Assert.Throws<OverflowException>(() => dummy.Create(length, state, action));
//     }
}