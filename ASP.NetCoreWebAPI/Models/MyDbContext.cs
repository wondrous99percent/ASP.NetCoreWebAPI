using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ASP.NetCoreWebAPICRUD.Models;
//MyDBContext and sql server should be registered in program.cs file
public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Student> Student { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {

        }
    }
    public async Task<Student?> GetStudentByStoredProcAsync(int id)
    {
        // Run the raw SQL, convert to list, then pick the first
        var result = await Student.FromSqlRaw("EXEC GetStudentById @StudentId = {0}", id).AsNoTracking().ToListAsync();

        return result.FirstOrDefault();
    }
    public DbSet<StudentLibraryDto> StudentLibraryRecords { get; set; }

    public async Task<StudentLibraryDto?> GetStudentLibraryRecordsAsync(int libraryId)
    {
        var result = await StudentLibraryRecords
            .FromSqlRaw("EXEC GetStudentLibraryRecords @LibraryId = {0}", libraryId)
            .AsNoTracking()
            .ToListAsync();
        return result.FirstOrDefault();

    }



}
