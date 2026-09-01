using AutoMapper;
using InterviewTest.Application.Common.Mapping;

namespace InterviewTest.Tests.Support;

public static class TestMapper
{
    public static IMapper Create()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<OrderProfile>();
            cfg.AddProfile<CustomerProfile>();
        });

        return configuration.CreateMapper();
    }
}
