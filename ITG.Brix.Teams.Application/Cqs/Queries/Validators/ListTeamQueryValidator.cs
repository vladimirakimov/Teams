using FluentValidation;
using ITG.Brix.Teams.Application.Bases;
using ITG.Brix.Teams.Application.Constants;
using ITG.Brix.Teams.Application.Cqs.Queries.Definitions;
using ITG.Brix.Teams.Application.Extensions;
using ITG.Brix.Teams.Application.Resources;
using System;

namespace ITG.Brix.Teams.Application.Cqs.Queries.Validators
{
    public class ListTeamQueryValidator : AbstractValidator<ListTeamQuery>
    {
        public ListTeamQueryValidator()
        {
            RuleFor(x => x.Top).Custom((elem, context) =>
            {
                if (elem != null)
                {
                    var top = 0;
                    var topMaxValue = Consts.CqsValidation.TopMaxValue;
                    string topRangeMessage = string.Format(CustomFailures.TopRange, topMaxValue);
                    try
                    {
                        top = Convert.ToInt32(elem);
                    }
                    catch (FormatException)
                    {
                        context.AddCustomFault("$top", CustomFaultCode.InvalidQueryTop, CustomFailures.TopInvalid);
                    }
                    catch (OverflowException)
                    {
                        context.AddCustomFault("$top", CustomFaultCode.InvalidQueryTop, topRangeMessage);
                    }
                    finally
                    {
                        if (top <= 0 || top > topMaxValue)
                        {
                            context.AddCustomFault("$top", CustomFaultCode.InvalidQueryTop, topRangeMessage);
                        }
                    }
                }
            });

            RuleFor(x => x.Skip).Custom((elem, context) =>
            {
                if (elem != null)
                {
                    var skip = 0;
                    var skipMaxValue = Consts.CqsValidation.SkipMaxValue;
                    string skipRangeMessage = string.Format(CustomFailures.SkipRange, skipMaxValue);
                    try
                    {
                        skip = Convert.ToInt32(elem);
                    }
                    catch (FormatException)
                    {
                        context.AddCustomFault("$skip", CustomFaultCode.InvalidQuerySkip, CustomFailures.SkipInvalid);
                    }
                    catch (OverflowException)
                    {
                        context.AddCustomFault("$skip", CustomFaultCode.InvalidQuerySkip, skipRangeMessage);
                    }
                    finally
                    {
                        if (skip < 0 || skip > skipMaxValue)
                        {
                            context.AddCustomFault("$skip", CustomFaultCode.InvalidQuerySkip, skipRangeMessage);
                        }
                    }
                }
            });
        }
    }
}
