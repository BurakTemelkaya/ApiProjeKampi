using ApiProjeKampi.WebAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiProjeKampi.WebAPI.Context;

public class ApiContext(DbContextOptions<ApiContext> options) : DbContext(options)
{
    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Chef> Chefs { get; set; }
    public virtual DbSet<Contact> Contacts { get; set; }
    public virtual DbSet<Feature> Features { get; set; }
    public virtual DbSet<Image> Images { get; set; }
    public virtual DbSet<Message> Messages { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Reservation> Reservations { get; set; }
    public virtual DbSet<Service> Services { get; set; }
    public virtual DbSet<Testimonial> Testimonials { get; set; }
    public virtual DbSet<YummyEvent> YummyEvents { get; set; }
    public virtual DbSet<Notification> Notifications { get; set; }
    public virtual DbSet<About> Abouts { get; set; }
    public virtual DbSet<EmployeeTask> EmployeeTasks { get; set; }
    public virtual DbSet<EmployeeTaskChef> EmployeeTaskChefs { get; set; }
    public virtual DbSet<GroupReservation> GroupReservations { get; set; }
}