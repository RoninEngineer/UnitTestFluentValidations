using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;

namespace UnitTestFluentValidations
{
    class Program
    {
        static void Main(string[] args)
        {

            var item = new Item(12345, 23456789, new DateTime(1900, 01, 01));
            var itemValidator = new ItemValidator();

            ValidationResult results = itemValidator.Validate(item);

            bool success = results.IsValid;
            IList<ValidationFailure> failures = results.Errors;

            Console.WriteLine(failures);
        }
    }

    public class Item
    {

        public Item()
        {

        }

        public Item(int clientId, long orgUid, DateTime checkDate)
        {
            _clientId = clientId;
            _orgUid = orgUid;
            _checkDate = checkDate;
        }

        private int _clientId { get; set; }
        private long _orgUid { get; set; }
        private DateTime _checkDate { get; set; }

        public int ClientId
        {
            get { return _clientId; }
            set { _clientId = value; }
        }

        public long OrgUid
        {
            get { return _orgUid; }
            set { _orgUid = value; }
        }

        public DateTime CheckDate
        {
            get { return _checkDate;}
            set { _checkDate = value; }
        }

        

    }

    public class ItemValidator : AbstractValidator<Item>
    {
        public ItemValidator()
        {
            RuleFor(x => x.ClientId).GreaterThan(0).NotNull().WithMessage("ClientId cannot be null");
            RuleFor(x => x.OrgUid).NotNull().GreaterThan(0).WithMessage("OrgUid cannot be null");
            RuleFor(x => x.CheckDate).Must(BeAValidDate).WithMessage("Check date must be a validate date");
        }

        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }

    }
}
