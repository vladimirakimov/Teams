using ITG.Brix.Teams.Domain.Exceptions;
using System;

namespace ITG.Brix.Teams.Domain.Internal
{
    /// <summary>
    ///     Error class for creating and unwrapping <see cref="Exception" /> instances.
    /// </summary>
    internal static class Error
    {
        /// <summary>
        ///     Creates an <see cref="ArgumentNullException" /> with the provided properties.
        /// </summary>
        /// <param name="messageFormat">A composite format string explaining the reason for the exception.</param>
        /// <param name="messageArgs">An object array that contains zero or more objects to format.</param>
        /// <returns>The logged <see cref="Exception" />.</returns>
        internal static ArgumentNullException ArgumentNull(string messageFormat, params object[] messageArgs)
        {
            return new ArgumentNullException(string.Format(messageFormat, messageArgs));
        }

        /// <summary>
        ///     Creates an <see cref="ArgumentException" /> with the provided properties.
        /// </summary>
        /// <param name="messageFormat">A composite format string explaining the reason for the exception.</param>
        /// <param name="messageArgs">An object array that contains zero or more objects to format.</param>
        /// <returns>The logged <see cref="Exception" />.</returns>
        internal static ArgumentException Argument(string messageFormat, params object[] messageArgs)
        {
            return new ArgumentException(string.Format(messageFormat, messageArgs));
        }

        /// <summary>
        ///     Creates an <see cref="InvalidIdentityException" /> with the provided properties.
        /// </summary>
        /// <param name="messageFormat">A composite format string explaining the reason for the exception.</param>
        /// <param name="messageArgs">An object array that contains zero or more objects to format.</param>
        /// <returns>The logged <see cref="Exception" />.</returns>
        internal static InvalidIdentityException InvalidIdentity(string messageFormat, params object[] messageArgs)
        {
            return new InvalidIdentityException(string.Format(messageFormat, messageArgs));
        }

        /// <summary>
        ///     Creates an <see cref="InvalidEnumerationException" /> with the provided properties.
        /// </summary>
        /// <param name="messageFormat">A composite format string explaining the reason for the exception.</param>
        /// <param name="messageArgs">An object array that contains zero or more objects to format.</param>
        /// <returns>The logged <see cref="Exception" />.</returns>
        internal static InvalidEnumerationException InvalidEnumeration(string messageFormat, params object[] messageArgs)
        {
            return new InvalidEnumerationException(string.Format(messageFormat, messageArgs));
        }



        /// <summary>
        ///     Creates an <see cref="NameShouldNotBeNullException" /> with the provided properties.
        /// </summary>
        /// <returns>The logged <see cref="Exception" />.</returns>
        internal static NameShouldNotBeNullException NameShouldNotBeNull()
        {
            return new NameShouldNotBeNullException();
        }

        /// <summary>
        ///     Creates an <see cref="NameFieldShouldNotBeEmptyException" /> with the provided properties.
        /// </summary>
        /// <returns>The logged <see cref="Exception" />.</returns>
        internal static NameFieldShouldNotBeEmptyException NameFieldShouldNotBeEmpty()
        {
            return new NameFieldShouldNotBeEmptyException();
        }

        /// <summary>
        ///     Creates an <see cref="DescriptionFieldShouldNotBeEmptyException" /> with the provided properties.
        /// </summary>
        /// <returns>The logged <see cref="Exception" />.</returns>
        internal static DescriptionFieldShouldNotBeEmptyException DescriptionFieldShouldNotBeEmpty()
        {
            return new DescriptionFieldShouldNotBeEmptyException();
        }

        /// <summary>
        ///     Creates an <see cref="LayoutFieldShouldNotBeDefaultGuidException" /> with the provided properties.
        /// </summary>
        /// <returns>The logged <see cref="Exception" />.</returns>
        internal static LayoutFieldShouldNotBeDefaultGuidException LayoutFieldShouldNotBeDefaultGuid()
        {
            return new LayoutFieldShouldNotBeDefaultGuidException();
        }

        /// <summary>
        ///     Creates an <see cref="MemberFieldShouldNotBeDefaultGuidException" /> with the provided properties.
        /// </summary>
        /// <returns>The logged <see cref="Exception" />.</returns>
        internal static MemberFieldShouldNotBeDefaultGuidException MemberFieldShouldNotBeDefaultGuid()
        {
            return new MemberFieldShouldNotBeDefaultGuidException();
        }

    }
}
