using Microsoft.EntityFrameworkCore;

namespace MxFace.Iris.SDK.Sample.Net.Data.Contexts;

public class MxFaceIrisSDKContext : DbContext
{
    public MxFaceIrisSDKContext(DbContextOptions<MxFaceIrisSDKContext> options) : base(options)
    {
        System.Diagnostics.Debug.WriteLine("MxFaceIrisSDKContext::ctor ->" + this.GetHashCode());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);

        MxFaceEntityFrameworkCoreHelpers.UseMxFaceIrisService(modelBuilder);
    }
}
