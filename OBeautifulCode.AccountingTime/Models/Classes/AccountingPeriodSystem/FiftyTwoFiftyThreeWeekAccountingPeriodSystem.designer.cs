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
    public partial class FiftyTwoFiftyThreeWeekAccountingPeriodSystem : IModel<FiftyTwoFiftyThreeWeekAccountingPeriodSystem>
    {
        /// <summary>
        /// Determines whether two objects of type <see cref="FiftyTwoFiftyThreeWeekAccountingPeriodSystem"/> are equal.
        /// </summary>
        /// <param name="left">The object to the left of the equality operator.</param>
        /// <param name="right">The object to the right of the equality operator.</param>
        /// <returns>true if the two items are equal; otherwise false.</returns>
        public static bool operator ==(FiftyTwoFiftyThreeWeekAccountingPeriodSystem left, FiftyTwoFiftyThreeWeekAccountingPeriodSystem right)
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
        /// Determines whether two objects of type <see cref="FiftyTwoFiftyThreeWeekAccountingPeriodSystem"/> are not equal.
        /// </summary>
        /// <param name="left">The object to the left of the equality operator.</param>
        /// <param name="right">The object to the right of the equality operator.</param>
        /// <returns>true if the two items are not equal; otherwise false.</returns>
        public static bool operator !=(FiftyTwoFiftyThreeWeekAccountingPeriodSystem left, FiftyTwoFiftyThreeWeekAccountingPeriodSystem right) => !(left == right);

        /// <inheritdoc />
        public bool Equals(FiftyTwoFiftyThreeWeekAccountingPeriodSystem other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (ReferenceEquals(other, null))
            {
                return false;
            }

            var result = this.LastDayOfWeekInAccountingYear.IsEqualTo(other.LastDayOfWeekInAccountingYear)
                      && this.AnchorMonth.IsEqualTo(other.AnchorMonth)
                      && this.FiftyTwoFiftyThreeWeekMethodology.IsEqualTo(other.FiftyTwoFiftyThreeWeekMethodology);

            return result;
        }

        /// <inheritdoc />
        public override bool Equals(object obj) => this == (obj as FiftyTwoFiftyThreeWeekAccountingPeriodSystem);

        /// <inheritdoc />
        public override int GetHashCode() => HashCodeHelper.Initialize()
            .Hash(this.LastDayOfWeekInAccountingYear)
            .Hash(this.AnchorMonth)
            .Hash(this.FiftyTwoFiftyThreeWeekMethodology)
            .Value;

        /// <inheritdoc />
        public new FiftyTwoFiftyThreeWeekAccountingPeriodSystem DeepClone() => (FiftyTwoFiftyThreeWeekAccountingPeriodSystem)this.DeepCloneInternal();

        /// <summary>
        /// Deep clones this object with a new <see cref="LastDayOfWeekInAccountingYear" />.
        /// </summary>
        /// <param name="lastDayOfWeekInAccountingYear">The new <see cref="LastDayOfWeekInAccountingYear" />.  This object will NOT be deep cloned; it is used as-is.</param>
        /// <returns>New <see cref="FiftyTwoFiftyThreeWeekAccountingPeriodSystem" /> using the specified <paramref name="lastDayOfWeekInAccountingYear" /> for <see cref="LastDayOfWeekInAccountingYear" /> and a deep clone of every other property.</returns>
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
        public FiftyTwoFiftyThreeWeekAccountingPeriodSystem DeepCloneWithLastDayOfWeekInAccountingYear(DayOfWeek lastDayOfWeekInAccountingYear)
        {
            var result = new FiftyTwoFiftyThreeWeekAccountingPeriodSystem(
                                 lastDayOfWeekInAccountingYear,
                                 this.AnchorMonth.DeepClone(),
                                 this.FiftyTwoFiftyThreeWeekMethodology.DeepClone());

            return result;
        }

        /// <summary>
        /// Deep clones this object with a new <see cref="AnchorMonth" />.
        /// </summary>
        /// <param name="anchorMonth">The new <see cref="AnchorMonth" />.  This object will NOT be deep cloned; it is used as-is.</param>
        /// <returns>New <see cref="FiftyTwoFiftyThreeWeekAccountingPeriodSystem" /> using the specified <paramref name="anchorMonth" /> for <see cref="AnchorMonth" /> and a deep clone of every other property.</returns>
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
        public FiftyTwoFiftyThreeWeekAccountingPeriodSystem DeepCloneWithAnchorMonth(MonthOfYear anchorMonth)
        {
            var result = new FiftyTwoFiftyThreeWeekAccountingPeriodSystem(
                                 this.LastDayOfWeekInAccountingYear.DeepClone(),
                                 anchorMonth,
                                 this.FiftyTwoFiftyThreeWeekMethodology.DeepClone());

            return result;
        }

        /// <summary>
        /// Deep clones this object with a new <see cref="FiftyTwoFiftyThreeWeekMethodology" />.
        /// </summary>
        /// <param name="fiftyTwoFiftyThreeWeekMethodology">The new <see cref="FiftyTwoFiftyThreeWeekMethodology" />.  This object will NOT be deep cloned; it is used as-is.</param>
        /// <returns>New <see cref="FiftyTwoFiftyThreeWeekAccountingPeriodSystem" /> using the specified <paramref name="fiftyTwoFiftyThreeWeekMethodology" /> for <see cref="FiftyTwoFiftyThreeWeekMethodology" /> and a deep clone of every other property.</returns>
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
        public FiftyTwoFiftyThreeWeekAccountingPeriodSystem DeepCloneWithFiftyTwoFiftyThreeWeekMethodology(FiftyTwoFiftyThreeWeekMethodology fiftyTwoFiftyThreeWeekMethodology)
        {
            var result = new FiftyTwoFiftyThreeWeekAccountingPeriodSystem(
                                 this.LastDayOfWeekInAccountingYear.DeepClone(),
                                 this.AnchorMonth.DeepClone(),
                                 fiftyTwoFiftyThreeWeekMethodology);

            return result;
        }

        /// <inheritdoc />
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        protected override AccountingPeriodSystem DeepCloneInternal()
        {
            var result = new FiftyTwoFiftyThreeWeekAccountingPeriodSystem(
                                 this.LastDayOfWeekInAccountingYear.DeepClone(),
                                 this.AnchorMonth.DeepClone(),
                                 this.FiftyTwoFiftyThreeWeekMethodology.DeepClone());

            return result;
        }

        /// <inheritdoc />
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        public override string ToString()
        {
            var result = Invariant($"OBeautifulCode.AccountingTime.FiftyTwoFiftyThreeWeekAccountingPeriodSystem: LastDayOfWeekInAccountingYear = {this.LastDayOfWeekInAccountingYear.ToString() ?? "<null>"}, AnchorMonth = {this.AnchorMonth.ToString() ?? "<null>"}, FiftyTwoFiftyThreeWeekMethodology = {this.FiftyTwoFiftyThreeWeekMethodology.ToString() ?? "<null>"}.");

            return result;
        }
    }
}