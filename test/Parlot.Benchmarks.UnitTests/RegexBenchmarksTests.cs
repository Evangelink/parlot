using Moq;
using Parlot;
using Parlot.Benchmarks;
using Parlot.Fluent;
using System;
using System.Reflection;
using System.Text.RegularExpressions;
using Xunit;

/// <summary>
/// Unit tests for the <see cref = "RegexBenchmarks"/> class focusing on testing the EmailRegexGenerated method.
/// </summary>
public class RegexBenchmarksTests
{
    /// <summary>
    /// Tests that the EmailRegexGenerated method returns a Regex instance that matches a valid email address.
    /// This test uses reflection to call the private static method and verifies that a known valid email is matched.
    /// </summary>
//     [Fact] [Error] (23-29)CS8600 Converting null literal or possible null value to non-nullable type.
//     public void EmailRegexGenerated_ValidEmail_ReturnsMatch()
//     {
//         // Arrange: Use reflection to retrieve the private static EmailRegexGenerated method.
//         MethodInfo method = typeof(RegexBenchmarks).GetMethod("EmailRegexGenerated", BindingFlags.NonPublic | BindingFlags.Static);
//         Assert.NotNull(method);
//         // Act: Invoke the method to get the Regex instance.
//         var regex = method.Invoke(null, null) as Regex;
//         // Assert: The regex should not be null and must match a valid email pattern.
//         Assert.NotNull(regex);
//         string validEmail = "test@example.com";
//         bool isMatch = regex.IsMatch(validEmail);
//         Assert.True(isMatch, $"The regex did not match the valid email: {validEmail}");
//     }

    /// <summary>
    /// Tests that the EmailRegexGenerated method returns a Regex instance that does not match an invalid email address.
    /// This test uses reflection to call the private static method and verifies that an improperly formatted email is not matched.
    /// </summary>
//     [Fact] [Error] (42-29)CS8600 Converting null literal or possible null value to non-nullable type.
//     public void EmailRegexGenerated_InvalidEmail_ReturnsNoMatch()
//     {
//         // Arrange: Retrieve EmailRegexGenerated method via reflection.
//         MethodInfo method = typeof(RegexBenchmarks).GetMethod("EmailRegexGenerated", BindingFlags.NonPublic | BindingFlags.Static);
//         Assert.NotNull(method);
//         // Act: Invoke the method to obtain the Regex.
//         var regex = method.Invoke(null, null) as Regex;
//         // Assert: The regex should not be null and must not match an invalid email.
//         Assert.NotNull(regex);
//         string invalidEmail = "invalid-email";
//         bool isMatch = regex.IsMatch(invalidEmail);
//         Assert.False(isMatch, $"The regex incorrectly matched the invalid email: {invalidEmail}");
//     }

    /// <summary>
    /// Tests that the EmailRegexGenerated method returns a Regex instance that correctly matches various valid email formats.
    /// This test uses reflection to access the private static method and then iterates over multiple valid email cases.
    /// </summary>
//     [Fact] [Error] (61-29)CS8600 Converting null literal or possible null value to non-nullable type.
//     public void EmailRegexGenerated_MultipleValidEmailFormats_ReturnsMatch()
//     {
//         // Arrange: Use reflection to access the EmailRegexGenerated method.
//         MethodInfo method = typeof(RegexBenchmarks).GetMethod("EmailRegexGenerated", BindingFlags.NonPublic | BindingFlags.Static);
//         Assert.NotNull(method);
//         // Act: Invoke the method to get the Regex instance.
//         var regex = method.Invoke(null, null) as Regex;
//         // Assert: Validate that the regex matches all of the provided valid email examples.
//         Assert.NotNull(regex);
//         string[] validEmails = new string[]
//         {
//             "user+alias@example.com",
//             "user.name@example.co.uk",
//             "user-name@sub.domain.com",
//             "user.name+tag+sorting@example.com"
//         };
//         foreach (var email in validEmails)
//         {
//             Assert.True(regex.IsMatch(email), $"The regex did not match the valid email: {email}");
//         }
//     }

    /// <summary>
    /// Verifies that the Setup method completes without throwing an exception when all benchmark methods return the expected Email.
    /// This test represents the happy path scenario.
    /// </summary>
    [Fact]
    public void Setup_WhenAllMethodsReturnExpectedEmail_DoesNotThrow()
    {
        // Arrange
        var benchmarks = new RegexBenchmarks();
        // Act & Assert
        var exception = Record.Exception(() => benchmarks.Setup());
        Assert.Null(exception);
    }

    /// <summary>
    /// Verifies that the Setup method throws an Exception with the correct message when the comparison for RegexEmail fails.
    /// This is achieved by modifying the static Email field to a mismatching value prior to calling Setup.
    /// Expected outcome: an Exception with Message equal to "RegexEmail" is thrown.
    /// </summary>
//     [Fact] [Error] (105-32)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (108-32)CS8600 Converting null literal or possible null value to non-nullable type.
//     public void Setup_WhenRegexEmailComparisonFails_ThrowsExceptionWithExpectedMessage()
//     {
//         // Arrange
//         var benchmarks = new RegexBenchmarks();
//         // Use reflection to get the static readonly field 'Email'
//         FieldInfo emailField = typeof(RegexBenchmarks).GetField("Email", BindingFlags.Static | BindingFlags.Public);
//         Assert.NotNull(emailField);
//         // Save the original Email value so that it can be restored after the test.
//         string originalEmail = emailField.GetValue(null)?.ToString();
//         try
//         {
//             // Change the expected Email value to force the first comparison to fail.
//             emailField.SetValue(null, "MismatchEmail");
//             // Act & Assert
//             Exception ex = Assert.Throws<Exception>(() => benchmarks.Setup());
//             Assert.Equal("RegexEmail", ex.Message);
//         }
//         finally
//         {
//             // Restore the original Email value to avoid side effects on other tests.
//             emailField.SetValue(null, originalEmail);
//         }
//     }

    private const string TestEmailPattern = "[\\w\\.+-]+@[\\w-]+\\.[\\w\\.-]+";
    /// <summary>
    /// A helper method to set a static field value in the <see cref = "RegexBenchmarks"/> class using reflection.
    /// </summary>
    /// <param name = "fieldName">The name of the field to set.</param>
    /// <param name = "value">The value to assign to the field.</param>
    private static void SetStaticField(string fieldName, object value)
    {
        var field = typeof(RegexBenchmarks).GetField(fieldName, BindingFlags.Public | BindingFlags.Static);
        if (field == null)
        {
            throw new InvalidOperationException($"Field '{fieldName}' not found in RegexBenchmarks.");
        }

        field.SetValue(null, value);
    }

    /// <summary>
    /// Tests the RegexEmailCompiled method to verify it returns the expected email when the input email is valid.
    /// Arrange: Sets the static Email field to a valid email and EmailRegexCompiled to a regex compiled with the expected pattern.
    /// Act: Calls RegexEmailCompiled.
    /// Assert: Verifies that the returned string equals the valid email provided.
    /// </summary>
    [Fact]
    public void RegexEmailCompiled_WithValidEmail_ReturnsEmail()
    {
        // Arrange
        string validEmail = "test.user@example.com";
        SetStaticField("Email", validEmail);
        var regex = new Regex(TestEmailPattern, RegexOptions.Compiled);
        SetStaticField("EmailRegexCompiled", regex);
        var benchmarks = new RegexBenchmarks();
        // Act
        string result = benchmarks.RegexEmailCompiled();
        // Assert
        Assert.Equal(validEmail, result);
    }

    /// <summary>
    /// Tests the RegexEmailCompiled method to verify it returns an empty string when the input email does not match the regex pattern.
    /// Arrange: Sets the static Email field to a string that does not match the email regex and EmailRegexCompiled to a regex compiled with the expected pattern.
    /// Act: Calls RegexEmailCompiled.
    /// Assert: Verifies that the returned string is empty.
    /// </summary>
    [Fact]
    public void RegexEmailCompiled_WithNonMatchingEmail_ReturnsEmpty()
    {
        // Arrange
        string nonMatchingEmail = "invalid-email";
        SetStaticField("Email", nonMatchingEmail);
        var regex = new Regex(TestEmailPattern, RegexOptions.Compiled);
        SetStaticField("EmailRegexCompiled", regex);
        var benchmarks = new RegexBenchmarks();
        // Act
        string result = benchmarks.RegexEmailCompiled();
        // Assert
        Assert.Equal(string.Empty, result);
    }

    /// <summary>
    /// Tests the RegexEmailCompiled method to verify it throws an ArgumentNullException when the input email is null.
    /// Arrange: Sets the static Email field to null and EmailRegexCompiled to a regex compiled with the expected pattern.
    /// Act & Assert: Expects the method to throw an ArgumentNullException.
    /// </summary>
//     [Fact] [Error] (192-33)CS8625 Cannot convert null literal to non-nullable reference type.
//     public void RegexEmailCompiled_WithNullEmail_ThrowsArgumentNullException()
//     {
//         // Arrange
//         SetStaticField("Email", null);
//         var regex = new Regex(TestEmailPattern, RegexOptions.Compiled);
//         SetStaticField("EmailRegexCompiled", regex);
//         var benchmarks = new RegexBenchmarks();
//         // Act & Assert
//         Assert.Throws<ArgumentNullException>(() => benchmarks.RegexEmailCompiled());
//     }

    private const string EmailPattern = "[\\w\\.+-]+@[\\w-]+\\.[\\w\\.-]+";
    /// <summary>
    /// Tests the RegexEmail method with a valid email string.
    /// This test sets the static EmailRegex and Email fields to valid values,
    /// invokes the RegexEmail method and verifies that it returns the matched email.
    /// </summary>
    [Fact]
    public void RegexEmail_ValidEmail_ReturnsEmail()
    {
        // Arrange
        var validEmail = "user@example.com";
        var expected = validEmail;
        var regex = new Regex(EmailPattern);
        SetStaticField(typeof(RegexBenchmarks), "EmailRegex", regex);
        SetStaticField(typeof(RegexBenchmarks), "Email", validEmail);
        var benchmarks = new RegexBenchmarks();
        // Act
        string result = benchmarks.RegexEmail();
        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests the RegexEmail method with an invalid email string.
    /// This test sets the static EmailRegex and Email fields so that the regex does not match,
    /// invokes the RegexEmail method and verifies that it returns an empty string.
    /// </summary>
    [Fact]
    public void RegexEmail_InvalidEmail_ReturnsEmptyString()
    {
        // Arrange
        var invalidEmail = "not-an-email";
        var expected = string.Empty;
        var regex = new Regex(EmailPattern);
        SetStaticField(typeof(RegexBenchmarks), "EmailRegex", regex);
        SetStaticField(typeof(RegexBenchmarks), "Email", invalidEmail);
        var benchmarks = new RegexBenchmarks();
        // Act
        string result = benchmarks.RegexEmail();
        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Sets a static readonly field of a given type via reflection.
    /// </summary>
    /// <param name = "type">The type containing the static field.</param>
    /// <param name = "fieldName">Name of the static field.</param>
    /// <param name = "value">Value to assign to the field.</param>
    private static void SetStaticField(Type type, string fieldName, object value)
    {
        var field = type.GetField(fieldName, BindingFlags.Public | BindingFlags.Static);
        if (field == null)
        {
            throw new ArgumentException($"Field '{fieldName}' not found in type '{type.FullName}'.");
        }

        field.SetValue(null, value);
    }

    /// <summary>
    /// A dummy parser implementation that returns a new TextSpan based on the input.
    /// Assumes that the <see cref = "TextSpan"/> type has a public constructor that accepts a string.
    /// </summary>
    /// <param name = "input">The input TextSpan.</param>
    /// <returns>A new TextSpan instance with the same string content.</returns>
//     private static TextSpan DummyParser(TextSpan input) [Error] (268-22)CS8625 Cannot convert null literal to non-nullable reference type.
//     {
//         if (input == null)
//         {
//             throw new ArgumentNullException(nameof(input));
//         }
// 
//         // Return a new TextSpan constructed from the string representation of the input.
//         return new TextSpan(input.ToString());
//     }

    /// <summary>
    /// Tests that <see cref = "RegexBenchmarks.ParlotEmailCompiled"/> returns the correctly parsed email
    /// when both the Email field and EmailCompiled parser are set to valid non-null values.
    /// </summary>
//     [Fact] [Error] (294-47)CS0030 Cannot convert type 'method' to 'Parser<TextSpan>' [Error] (286-32)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (287-40)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (288-32)CS8602 Dereference of a possibly null reference. [Error] (288-32)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (289-40)CS8602 Dereference of a possibly null reference. [Error] (289-40)CS8600 Converting null literal or possible null value to non-nullable type.
//     public void ParlotEmailCompiled_ValidEmail_ReturnsParsedEmail()
//     {
//         // Arrange
//         string testEmail = "test@example.com";
//         FieldInfo emailField = typeof(RegexBenchmarks).GetField("Email", BindingFlags.Public | BindingFlags.Static);
//         FieldInfo emailCompiledField = typeof(RegexBenchmarks).GetField("EmailCompiled", BindingFlags.Public | BindingFlags.Static);
//         object originalEmail = emailField.GetValue(null);
//         object originalEmailCompiled = emailCompiledField.GetValue(null);
//         try
//         {
//             emailField.SetValue(null, testEmail);
//             // Set the EmailCompiled static field to our dummy parser.
//             emailCompiledField.SetValue(null, (Parser<TextSpan>)DummyParser);
//             var benchmarks = new RegexBenchmarks();
//             // Act
//             TextSpan result = benchmarks.ParlotEmailCompiled();
//             // Assert
//             Assert.NotNull(result);
//             Assert.Equal(testEmail, result.ToString());
//         }
//         finally
//         {
//             // Restore original static field values.
//             emailField.SetValue(null, originalEmail);
//             emailCompiledField.SetValue(null, originalEmailCompiled);
//         }
//     }

    /// <summary>
    /// Tests that <see cref = "RegexBenchmarks.ParlotEmailCompiled"/> throws an exception
    /// when the static Email field is set to null.
    /// </summary>
//     [Fact] [Error] (326-47)CS0030 Cannot convert type 'method' to 'Parser<TextSpan>' [Error] (318-32)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (319-40)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (320-32)CS8602 Dereference of a possibly null reference. [Error] (320-32)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (321-40)CS8602 Dereference of a possibly null reference. [Error] (321-40)CS8600 Converting null literal or possible null value to non-nullable type.
//     public void ParlotEmailCompiled_NullEmail_ThrowsException()
//     {
//         // Arrange
//         FieldInfo emailField = typeof(RegexBenchmarks).GetField("Email", BindingFlags.Public | BindingFlags.Static);
//         FieldInfo emailCompiledField = typeof(RegexBenchmarks).GetField("EmailCompiled", BindingFlags.Public | BindingFlags.Static);
//         object originalEmail = emailField.GetValue(null);
//         object originalEmailCompiled = emailCompiledField.GetValue(null);
//         try
//         {
//             // Set Email to null.
//             emailField.SetValue(null, null);
//             emailCompiledField.SetValue(null, (Parser<TextSpan>)DummyParser);
//             var benchmarks = new RegexBenchmarks();
//             // Act & Assert
//             Assert.Throws<NullReferenceException>(() => benchmarks.ParlotEmailCompiled());
//         }
//         finally
//         {
//             // Restore original static field values.
//             emailField.SetValue(null, originalEmail);
//             emailCompiledField.SetValue(null, originalEmailCompiled);
//         }
//     }

    /// <summary>
    /// Tests that <see cref = "RegexBenchmarks.ParlotEmailCompiled"/> throws an exception
    /// when the static EmailCompiled parser field is set to null.
    /// </summary>
//     [Fact] [Error] (348-32)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (349-40)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (350-32)CS8602 Dereference of a possibly null reference. [Error] (350-32)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (351-40)CS8602 Dereference of a possibly null reference. [Error] (351-40)CS8600 Converting null literal or possible null value to non-nullable type.
//     public void ParlotEmailCompiled_NullParser_ThrowsException()
//     {
//         // Arrange
//         string testEmail = "test@example.com";
//         FieldInfo emailField = typeof(RegexBenchmarks).GetField("Email", BindingFlags.Public | BindingFlags.Static);
//         FieldInfo emailCompiledField = typeof(RegexBenchmarks).GetField("EmailCompiled", BindingFlags.Public | BindingFlags.Static);
//         object originalEmail = emailField.GetValue(null);
//         object originalEmailCompiled = emailCompiledField.GetValue(null);
//         try
//         {
//             emailField.SetValue(null, testEmail);
//             // Set EmailCompiled parser to null.
//             emailCompiledField.SetValue(null, null);
//             var benchmarks = new RegexBenchmarks();
//             // Act & Assert
//             Assert.Throws<NullReferenceException>(() => benchmarks.ParlotEmailCompiled());
//         }
//         finally
//         {
//             // Restore original static field values.
//             emailField.SetValue(null, originalEmail);
//             emailCompiledField.SetValue(null, originalEmailCompiled);
//         }
//     }

    /// <summary>
    /// A stub parser to simulate the behavior of <see cref = "Parser{TextSpan}"/> for testing purposes.
    /// </summary>
    private class StubParser
    {
        private readonly Func<string, TextSpan> _parseFunc;
        /// <summary>
        /// Initializes a new instance of the <see cref = "StubParser"/> class with a provided parsing function.
        /// </summary>
        /// <param name = "parseFunc">The function that simulates parsing.</param>
        public StubParser(Func<string, TextSpan> parseFunc)
        {
            _parseFunc = parseFunc;
        }

        /// <summary>
        /// Parses the provided input and returns a <see cref = "TextSpan"/>.
        /// </summary>
        /// <param name = "input">The input string to parse.</param>
        /// <returns>A <see cref = "TextSpan"/> representing the parsed input.</returns>
        public TextSpan Parse(string input)
        {
            return _parseFunc(input);
        }
    }

    /// <summary>
    /// Tests that the <see cref = "RegexBenchmarks.ParlotEmail"/> method returns the expected TextSpan when the email is valid.
    /// </summary>
//     [Fact] [Error] (405-38)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (406-32)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (408-9)CS8602 Dereference of a possibly null reference. [Error] (411-9)CS8602 Dereference of a possibly null reference.
//     public void ParlotEmail_WhenCalledWithValidEmail_ReturnsExpectedTextSpan()
//     {
//         // Arrange
//         string testEmail = "test@example.com";
//         TextSpan expectedTextSpan = new TextSpan(testEmail);
//         // Use reflection to override the static readonly fields EmailParser and Email.
//         FieldInfo emailParserField = typeof(RegexBenchmarks).GetField("EmailParser", BindingFlags.Public | BindingFlags.Static);
//         FieldInfo emailField = typeof(RegexBenchmarks).GetField("Email", BindingFlags.Public | BindingFlags.Static);
//         // Set Email to testEmail.
//         emailField.SetValue(null, testEmail);
//         // Set EmailParser to a stub that returns a TextSpan constructed with the input.
//         var stubParser = new StubParser(input => new TextSpan(input));
//         emailParserField.SetValue(null, stubParser);
//         var benchmarks = new RegexBenchmarks();
//         // Act
//         TextSpan result = benchmarks.ParlotEmail();
//         // Assert
//         Assert.Equal(expectedTextSpan, result);
//     }

    /// <summary>
    /// Tests that the <see cref = "RegexBenchmarks.ParlotEmail"/> method propagates exceptions thrown by the parser.
    /// </summary>
//     [Fact] [Error] (428-38)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (429-32)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (431-9)CS8602 Dereference of a possibly null reference. [Error] (434-9)CS8602 Dereference of a possibly null reference.
//     public void ParlotEmail_WhenParserThrowsException_PropagatesException()
//     {
//         // Arrange
//         string testEmail = "invalid-email";
//         // Use reflection to override the static readonly fields EmailParser and Email.
//         FieldInfo emailParserField = typeof(RegexBenchmarks).GetField("EmailParser", BindingFlags.Public | BindingFlags.Static);
//         FieldInfo emailField = typeof(RegexBenchmarks).GetField("Email", BindingFlags.Public | BindingFlags.Static);
//         // Set Email to testEmail.
//         emailField.SetValue(null, testEmail);
//         // Set EmailParser to a stub that always throws an exception when Parse is called.
//         var stubParser = new StubParser(input => throw new InvalidOperationException("Parsing failure."));
//         emailParserField.SetValue(null, stubParser);
//         var benchmarks = new RegexBenchmarks();
//         // Act & Assert
//         InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => benchmarks.ParlotEmail());
//         Assert.Equal("Parsing failure.", exception.Message);
//     }
}