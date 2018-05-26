using System;
using System.Windows.Forms;

namespace Settings.Sources.Validator
{
    public enum ValidatorDataResult
    {
        Ok,
        Disable,
        Error,
        Warning,
    };
    public interface IValidatorData
    {
        String message
        {
            get;
        }
        ValidatorDataResult result
        {
            get;
        }
    }

    public class ValidatorData : IValidatorData
    {
        public String message
        {
            get { return this.m_message; }
            set { this.m_message = value; }
        }

        public ValidatorDataResult result
        {
            get { return this.m_result; }
            set { this.m_result = value; }
        }

        private String m_message = null;
        private ValidatorDataResult m_result = ValidatorDataResult.Ok;
    }

    public interface IValidator
    {
       event EventHandler Action;

       IValidatorData validate();
    }

}
