﻿// --------------------------------------------------------------------------------------------------------------------
// <auto-generated>
//   Generated using OBeautifulCode.CodeGen.ModelObject (1.0.181.0)
// </auto-generated>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AccountingTime
{
    using global::System;
    using global::System.CodeDom.Compiler;
    using global::System.Collections.Concurrent;
    using global::System.Collections.Generic;
    using global::System.Collections.ObjectModel;
    using global::System.Diagnostics.CodeAnalysis;
    using global::System.Globalization;
    using global::System.Linq;

    using global::OBeautifulCode.Cloning.Recipes;
    using global::OBeautifulCode.Equality.Recipes;
    using global::OBeautifulCode.Type;
    using global::OBeautifulCode.Type.Recipes;

    using static global::System.FormattableString;

    [Serializable]
    public partial class GenericYear : IModel<GenericYear>, IComparableForRelativeSortOrder<GenericYear>
    {
        /// <summary>
        /// Determines whether two objects of type <see cref="GenericYear"/> are equal.
        /// </summary>
        /// <param name="left">The object to the left of the equality operator.</param>
        /// <param name="right">The object to the right of the equality operator.</param>
        /// <returns>true if the two items are equal; otherwise false.</returns>
        public static bool operator ==(GenericYear left, GenericYear right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }

            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
            {
                return false;
            }

            var result = left.Equals(right);

            return result;
        }

        /// <summary>
        /// Determines whether two objects of type <see cref="GenericYear"/> are not equal.
        /// </summary>
        /// <param name="left">The object to the left of the equality operator.</param>
        /// <param name="right">The object to the right of the equality operator.</param>
        /// <returns>true if the two items are not equal; otherwise false.</returns>
        public static bool operator !=(GenericYear left, GenericYear right) => !(left == right);

        /// <inheritdoc />
        public bool Equals(GenericYear other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (ReferenceEquals(other, null))
            {
                return false;
            }

            var result = this.Year.IsEqualTo(other.Year);

            return result;
        }

        /// <inheritdoc />
        public override bool Equals(object obj) => this == (obj as GenericYear);

        /// <summary>
        /// Determines whether an object of type <see cref="GenericYear"/> is less than another object of that type.
        /// </summary>
        /// <param name="left">The object to the left of the less-than operator.</param>
        /// <param name="right">The object to the right of the less-than operator.</param>
        /// <returns>true if <paramref name="left"/> is less than <paramref name="right"/>; otherwise false.</returns>
        public static bool operator <(GenericYear left, GenericYear right)
        {
            if (ReferenceEquals(left, right))
            {
                return false;
            }

            if (ReferenceEquals(left, null))
            {
                return true;
            }

            if (ReferenceEquals(right, null))
            {
                return false;
            }

            if (left.GetType() != right.GetType())
            {
                throw new ArgumentException(Invariant($"Attempting to compare objects of different types.  The left operand is of type '{left.GetType().ToStringReadable()}' whereas the right operand is of type '{right.GetType().ToStringReadable()}'."));
            }

            var relativeSortOrder = left.CompareToForRelativeSortOrder(right);

            var result = relativeSortOrder == RelativeSortOrder.ThisInstancePrecedesTheOtherInstance;

            return result;
        }

        /// <summary>
        /// Determines whether an object of type <see cref="GenericYear"/> is greater than another object of that type.
        /// </summary>
        /// <param name="left">The object to the left of the greater-than operator.</param>
        /// <param name="right">The object to the right of the greater-than operator.</param>
        /// <returns>true if <paramref name="left"/> is greater than <paramref name="right"/>; otherwise false.</returns>
        public static bool operator >(GenericYear left, GenericYear right)
        {
            if (ReferenceEquals(left, right))
            {
                return false;
            }

            if (ReferenceEquals(left, null))
            {
                return false;
            }

            if (ReferenceEquals(right, null))
            {
                return true;
            }

            if (left.GetType() != right.GetType())
            {
                throw new ArgumentException(Invariant($"Attempting to compare objects of different types.  The left operand is of type '{left.GetType().ToStringReadable()}' whereas the right operand is of type '{right.GetType().ToStringReadable()}'."));
            }

            var relativeSortOrder = left.CompareToForRelativeSortOrder(right);

            var result = relativeSortOrder == RelativeSortOrder.ThisInstanceFollowsTheOtherInstance;

            return result;
        }

        /// <summary>
        /// Determines whether an object of type <see cref="GenericYear"/> is less than or equal to another object of that type.
        /// </summary>
        /// <param name="left">The object to the left of the less-than-or-equal-to operator.</param>
        /// <param name="right">The object to the right of the less-than-or-equal-to operator.</param>
        /// <returns>true if <paramref name="left"/> is less than or equal to <paramref name="right"/>; otherwise false.</returns>
        public static bool operator <=(GenericYear left, GenericYear right) => !(left > right);

        /// <summary>
        /// Determines whether an object of type <see cref="GenericYear"/> is greater than or equal to another object of that type.
        /// </summary>
        /// <param name="left">The object to the left of the greater-than-or-equal-to operator.</param>
        /// <param name="right">The object to the right of the greater-than-or-equal-to operator.</param>
        /// <returns>true if <paramref name="left"/> is greater than or equal to <paramref name="right"/>; otherwise false.</returns>
        public static bool operator >=(GenericYear left, GenericYear right) => !(left < right);

        /// <inheritdoc />
        public int CompareTo(GenericYear other)
        {
            if (ReferenceEquals(other, null))
            {
                return 1;
            }

            var relativeSortOrder = this.CompareToForRelativeSortOrder(other);

            switch(relativeSortOrder)
            {
                case RelativeSortOrder.ThisInstancePrecedesTheOtherInstance:
                    return -1;
                case RelativeSortOrder.ThisInstanceOccursInTheSamePositionAsTheOtherInstance:
                    return 0;
                case RelativeSortOrder.ThisInstanceFollowsTheOtherInstance:
                    return 1;
                default:
                    throw new NotSupportedException(Invariant($"This {nameof(RelativeSortOrder)} is not supported: {relativeSortOrder}."));
            }
        }

        /// <inheritdoc />
        public override int CompareTo(object obj)
        {
            if (ReferenceEquals(obj, null))
            {
                return 1;
            }

            if (!(obj is GenericYear other))
            {
                throw new ArgumentException(Invariant($"Attempting to compare objects of different types.  This object is of type 'GenericYear' whereas the other object is of type '{obj.GetType().ToStringReadable()}'."));
            }

            var result = this.CompareTo(other);

            return result;
        }

        /// <inheritdoc />
        public override RelativeSortOrder CompareToForRelativeSortOrder(UnitOfTime other)
        {
            if (ReferenceEquals(other, null))
            {
                return RelativeSortOrder.ThisInstanceFollowsTheOtherInstance;
            }

            if (!(other is GenericYear otherAsGenericYear))
            {
                throw new ArgumentException(Invariant($"Attempting to compare objects of different types.  This object is of type 'GenericYear' whereas the other object is of type '{other.GetType().ToStringReadable()}'."));
            }

            var result = this.CompareToForRelativeSortOrder(otherAsGenericYear);

            return result;
        }

        /// <inheritdoc />
        public override RelativeSortOrder CompareToForRelativeSortOrder(GenericUnitOfTime other)
        {
            if (ReferenceEquals(other, null))
            {
                return RelativeSortOrder.ThisInstanceFollowsTheOtherInstance;
            }

            if (!(other is GenericYear otherAsGenericYear))
            {
                throw new ArgumentException(Invariant($"Attempting to compare objects of different types.  This object is of type 'GenericYear' whereas the other object is of type '{other.GetType().ToStringReadable()}'."));
            }

            var result = this.CompareToForRelativeSortOrder(otherAsGenericYear);

            return result;
        }

        /// <inheritdoc />
        public override int GetHashCode() => HashCodeHelper.Initialize()
            .Hash(this.Year)
            .Value;

        /// <inheritdoc />
        public new GenericYear DeepClone() => (GenericYear)this.DeepCloneInternal();

        /// <summary>
        /// Deep clones this object with a new <see cref="Year" />.
        /// </summary>
        /// <param name="year">The new <see cref="Year" />.  This object will NOT be deep cloned; it is used as-is.</param>
        /// <returns>New <see cref="GenericYear" /> using the specified <paramref name="year" /> for <see cref="Year" /> and a deep clone of every other property.</returns>
        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings")]
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
        [SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")]
        [SuppressMessage("Microsoft.Naming", "CA1715:IdentifiersShouldHaveCorrectPrefix")]
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords")]
        [SuppressMessage("Microsoft.Naming", "CA1719:ParameterNamesShouldNotMatchMemberNames")]
        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames")]
        [SuppressMessage("Microsoft.Naming", "CA1722:IdentifiersShouldNotHaveIncorrectPrefix")]
        [SuppressMessage("Microsoft.Naming", "CA1725:ParameterNamesShouldMatchBaseDeclaration")]
        [SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms")]
        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly")]
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public GenericYear DeepCloneWithYear(int year)
        {
            var result = new GenericYear(
                                 year);

            return result;
        }

        /// <inheritdoc />
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        protected override UnitOfTime DeepCloneInternal()
        {
            var result = new GenericYear(
                                 this.Year.DeepClone());

            return result;
        }
    }
}