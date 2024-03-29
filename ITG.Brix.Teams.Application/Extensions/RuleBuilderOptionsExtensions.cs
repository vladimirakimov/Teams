﻿using FluentValidation;
using ITG.Brix.Teams.Application.Bases;
using ITG.Brix.Teams.Application.Enums;

namespace ITG.Brix.Teams.Application.Extensions
{
    public static class RuleBuilderOptionsExtensions
    {
        public static IRuleBuilderOptions<T, TProperty> CustomFault<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule, CustomFaultCode errorCode, string errorMessage)
        {
            return rule.WithMessage(errorMessage).WithErrorCode(ErrorType.CustomError.ToString() + "###" + errorCode.Name);
        }

        public static IRuleBuilderOptions<T, TProperty> ValidationFault<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule, string errorMessage)
        {
            return rule.WithMessage(errorMessage);
        }
    }
}
