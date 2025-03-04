﻿using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;

namespace CashFlow.Application.AutoMapper;
public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToEntity();
        EntityToResponse();
    }

    private void RequestToEntity()
    {
        CreateMap<RequestExpenseJson, Expense>();
        CreateMap<RequestRegisterUserJson, User>()
            .ForMember(user => user.Password, opt => opt.Ignore());
    }

    private void EntityToResponse()
    {
        CreateMap<Expense, ResponseRegisteredExpensiveJson>();
        CreateMap<Expense, ResponseShortExpensesJson>();
        CreateMap<Expense, ResponseExpenseJson>();
    }
}
