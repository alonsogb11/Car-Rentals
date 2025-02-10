using Microsoft.EntityFrameworkCore;
using RentACar.Models;

namespace RentACar.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
            
        }

        public DbSet<TipoVehiculo> TipoVehiculos { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Modelo> Modelos { get; set; }
        public DbSet<TipoCombustible> TipoCombustibles { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<ProcesoRentaDevolucion> ProcesosRentaDevolucion { get; set; }
        public DbSet<Inspeccion> Inspecciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TipoVehiculo>(tb =>
            {
                tb.HasKey(col => col.Id);
                tb.Property(col => col.Id).UseIdentityColumn().ValueGeneratedOnAdd();

                tb.Property(col => col.Descripcion).HasMaxLength(50);

                tb.Property(col => col.Estado).HasConversion<String>();
            });

            //---------------------------------------------------------

            modelBuilder.Entity<Marca>(tb =>
            {
                tb.HasKey(col => col.Id);
                tb.Property(col => col.Id).UseIdentityColumn().ValueGeneratedOnAdd();

                tb.Property(col => col.Descripcion).HasMaxLength(50);

                tb.Property(col => col.Estado).HasConversion<String>();
            });

            //---------------------------------------------------------

            modelBuilder.Entity<Modelo>(tb =>
            {
                tb.HasKey(col => col.Id);
                tb.Property(col => col.Id).UseIdentityColumn().ValueGeneratedOnAdd();

                tb.HasOne(m => m.Marca).WithMany().HasForeignKey(m => m.MarcaId).OnDelete(DeleteBehavior.Cascade);

                tb.Property(col => col.Descripcion).HasMaxLength(50);

                tb.Property(col => col.Estado).HasConversion<String>();
            });

            //---------------------------------------------------------

            modelBuilder.Entity<TipoCombustible>(tb =>
            {
                tb.HasKey(col => col.Id);
                tb.Property(col => col.Id).UseIdentityColumn().ValueGeneratedOnAdd();

                tb.Property(col => col.Descripcion).HasMaxLength(50);

                tb.Property(col => col.Estado).HasConversion<String>();
            });

            //---------------------------------------------------------

            modelBuilder.Entity<Vehiculo>(tb =>
            {
                tb.HasKey(col => col.Id);
                tb.Property(col => col.Id).UseIdentityColumn().ValueGeneratedOnAdd();

                tb.Property(col => col.Descripcion).HasMaxLength(100);

                tb.Property(col => col.NumChasis).HasMaxLength(50);

                tb.Property(col => col.Motor).HasMaxLength(50);

                tb.Property(col => col.NumPlaca).HasMaxLength(7);

                tb.Property(col => col.Estado).HasConversion<string>();

                tb.HasOne(v => v.TipoVehiculo).WithMany().HasForeignKey(v => v.TipoVehiculoId).OnDelete(DeleteBehavior.Restrict);

                tb.HasOne(v => v.Marca).WithMany().HasForeignKey(v => v.MarcaId).OnDelete(DeleteBehavior.Restrict);

                tb.HasOne(v => v.Modelo).WithMany().HasForeignKey(v => v.ModeloId).OnDelete(DeleteBehavior.Restrict);

                tb.HasOne(v => v.TipoCombustible).WithMany().HasForeignKey(v => v.TipoCombustibleId).OnDelete(DeleteBehavior.Restrict);
            });

            //---------------------------------------------------------

            modelBuilder.Entity<Cliente>(tb =>
            {
                tb.HasKey(col => col.Id);
                tb.Property(col => col.Id).UseIdentityColumn().ValueGeneratedOnAdd();

                tb.Property(col => col.Nombre).HasMaxLength(50);

                tb.HasIndex(col => col.Cedula).IsUnique();

                tb.Property(col => col.NumTarjetaCredito).HasMaxLength(50);

                tb.Property(col => col.TipoPersona).HasConversion<String>();

                tb.Property(col => col.Estado).HasConversion<String>();
            });

            //---------------------------------------------------------

            modelBuilder.Entity<Empleado>(tb =>
            {
                tb.HasKey(col => col.Id);
                tb.Property(col => col.Id).UseIdentityColumn().ValueGeneratedOnAdd();

                tb.Property(col => col.Nombre).HasMaxLength(50);

                tb.HasIndex(col => col.Cedula).IsUnique();

                tb.Property(col => col.TandaLabor).HasConversion<String>();

                tb.Property(col => col.PorcientoComision).HasColumnType("decimal(5, 2)");

                tb.Property(col => col.FechaContratacion).HasColumnType("date");

                tb.Property(col => col.Estado).HasConversion<String>();
            });

            //---------------------------------------------------------

            modelBuilder.Entity<ProcesoRentaDevolucion>(tb =>
            {
                tb.HasKey(col => col.Id);
                tb.Property(col => col.Id).UseIdentityColumn().ValueGeneratedOnAdd();

                tb.HasOne(col => col.Empleado).WithMany().HasForeignKey(col => col.EmpleadoId).OnDelete(DeleteBehavior.Restrict);

                tb.HasOne(col => col.Vehiculo).WithMany().HasForeignKey(col => col.VehiculoId).OnDelete(DeleteBehavior.Restrict);

                tb.HasOne(col => col.Cliente).WithMany().HasForeignKey(col => col.ClienteId).OnDelete(DeleteBehavior.Restrict);

                tb.Property(col => col.FechaRenta).HasColumnType("date");

                tb.Property(col => col.FechaDevolucion).HasColumnType("date");

                tb.Property(col => col.MontoXDia).HasColumnType("decimal(5, 2)");

                tb.Property(col => col.CantidadDias).HasColumnType("int");

                tb.Property(col => col.Comentario).HasMaxLength(500);

                tb.Property(col => col.Estado).HasConversion<String>();
            });

            //---------------------------------------------------------

            modelBuilder.Entity<Inspeccion>(tb =>
            {
                tb.HasKey(col => col.Id);
                tb.Property(col => col.Id).UseIdentityColumn().ValueGeneratedOnAdd();

                tb.HasOne(col => col.ProcesoRentaDevolucion).WithMany().HasForeignKey(col => col.ProcesoRentaDevolucionId).OnDelete(DeleteBehavior.Restrict);

                tb.HasOne(col => col.Vehiculo).WithMany().HasForeignKey(col => col.VehiculoId).OnDelete(DeleteBehavior.Restrict);

                tb.HasOne(col => col.Cliente).WithMany().HasForeignKey(i => i.ClienteId).OnDelete(DeleteBehavior.Restrict);

                tb.Property(col => col.CantCombustible).HasConversion<String>();

                tb.Property(col => col.TieneRayones).HasColumnType("bit");

                tb.Property(col => col.TieneGomaRepuesta).HasColumnType("bit");

                tb.Property(col => col.TieneGato).HasColumnType("bit");

                tb.Property(col => col.TieneCristalRoto).HasColumnType("bit");

                tb.HasOne(col => col.Empleado).WithMany().HasForeignKey(i => i.EmpleadoId).OnDelete(DeleteBehavior.Restrict);

                tb.Property(col => col.Estado).HasConversion<String>();
            });
        }
    }
}
