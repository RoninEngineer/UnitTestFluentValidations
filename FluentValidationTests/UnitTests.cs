using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UnitTestFluentValidations;

namespace FluentValidationTests
{
    [TestClass]
    public class FluentValidationTests
    {
        private ItemValidator validator;

        [TestInitialize]
        public void TestInitialize()
        {
            validator = new ItemValidator();
        }

        [TestMethod]
        public void Validate_ClientId_Is_Null()
        {
            var item = new Item { OrgUid = 123456, CheckDate = new DateTime(2020,4,1)};

            var result = validator.TestValidate(item);
            result.ShouldHaveValidationErrorFor(x => x.ClientId);
        }

        [TestMethod]
        public void Validate_ClientId_Is_LessThanZero()
        {
            var item = new Item {ClientId = -1, OrgUid = 123456, CheckDate = new DateTime(2020, 4, 1) };

            var result = validator.TestValidate(item);
            result.ShouldHaveValidationErrorFor(x => x.ClientId);
        }

        [TestMethod]
        public void Validate_OrgUid_is_Null()
        {
            var item = new Item { ClientId = 12345,  CheckDate = new DateTime(2020, 4, 1) };

            var result = validator.TestValidate(item);
            result.ShouldHaveValidationErrorFor(x => x.OrgUid);
        }

        [TestMethod]
        public void Validate_OrgUid_is_LessThanZero()
        {
            var item = new Item { ClientId = 12345, OrgUid = -1000, CheckDate = new DateTime(2020, 4, 1) };

            var result = validator.TestValidate(item);
            result.ShouldHaveValidationErrorFor(x => x.OrgUid);
        }

        [TestMethod]
        public void Validate_CheckDate_is_InValid()
        {
            var item = new Item { ClientId = 12345, OrgUid = 321654987, CheckDate = default(DateTime) };

            var result = validator.TestValidate(item);
            result.ShouldHaveValidationErrorFor(x => x.CheckDate);
        }
    }
}
