using Domain;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services;
public class TestService
{
    private readonly ApplicationContext _db;

    public TestService(ApplicationContext db)
    {
        _db = db;
    }

    public IEnumerable<Test> GetTests()
    {
        return _db.Tests.AsEnumerable<Test>();
    }
}
