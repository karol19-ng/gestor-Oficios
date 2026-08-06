using Gestor_Oficios.Models.Entities;
using Gestor0ficios.Models.Entities;
using GestorOficios.Models.Entities;
using GestorOficios.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Gestor_Oficios.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DbSets
        public DbSet<Accesos_Temporales> Accesos_Temporales { get; set; }
        public DbSet<Acciones_Sobre_Archivos> Acciones_Sobre_Archivos { get; set; }
        public DbSet<Archivo> Archivos { get; set; }
        public DbSet<Auditoria_Archivos> Auditoria_Archivos { get; set; }
        public DbSet<Cargos> Cargos { get; set; }
        public DbSet<Control_Oficios> Control_Oficios { get; set; }
        public DbSet<Departamentos> Departamentos { get; set; }
        public DbSet<Estados_Solicitudes> Estados_Solicitudes { get; set; }
        public DbSet<Oficios_En_Progreso> Oficios_En_Progreso { get; set; }
        public DbSet<Permisos_Solicitud> Permisos_Solicitud { get; set; }
        public DbSet<Plantillas_Departamento> Plantillas_Departamento { get; set; }
        public DbSet<Prioridad_Solicitudes> Prioridad_Solicitudes { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Solicitudes> Solicitudes { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Correlativo_Oficio> Correlativos_Oficios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ============================================
            // USUARIOS
            // ============================================
            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.ToTable("Usuarios");
                entity.HasKey(e => e.Id_Usuario);

                entity.Property(e => e.Nombre_Completo).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Usuario).HasMaxLength(50).IsRequired();
                entity.HasIndex(e => e.Usuario).IsUnique();

                entity.Property(e => e.Contraseña_Hash).HasMaxLength(256).IsRequired();
                entity.Property(e => e.Estado).HasDefaultValue(true);
                entity.Property(e => e.Fecha_Creacion).HasDefaultValueSql("GETDATE()");

                // Relación con Departamento
                entity.HasOne(e => e.Departamentos)
                      .WithMany(d => d.Usuarios)
                      .HasForeignKey(e => e.Codigo_Departamento)
                      .OnDelete(DeleteBehavior.Restrict);

                // Relación con Cargo
                entity.HasOne(e => e.Cargo)
                      .WithMany(c => c.Usuarios)
                      .HasForeignKey(e => e.Id_Cargo)
                      .OnDelete(DeleteBehavior.Restrict);

                // Relación con Rol
                entity.HasOne(e => e.Rol)
                      .WithMany(r => r.Usuarios)
                      .HasForeignKey(e => e.Id_Rol)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ============================================
            // ROLES
            // ============================================
            modelBuilder.Entity<Roles>(entity =>
            {
                entity.ToTable("Roles");
                entity.HasKey(e => e.Id_Rol);
                entity.Property(e => e.Nombre).HasMaxLength(50).IsRequired();
            });

            // ============================================
            // CARGOS
            // ============================================
            modelBuilder.Entity<Cargos>(entity =>
            {
                entity.ToTable("Cargos");
                entity.HasKey(e => e.Id_Cargo);
                entity.Property(e => e.Nombre).HasMaxLength(50).IsRequired();
            });

            // ============================================
            // DEPARTAMENTOS
            // ============================================
            modelBuilder.Entity<Departamentos>(entity =>
            {
                entity.ToTable("Departamentos");
                entity.HasKey(e => e.Codigo);
                entity.Property(e => e.ID).HasMaxLength(10).IsRequired();
                entity.Property(e => e.Nombre).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Logo).HasColumnType("varbinary(max)");
            });

            // ============================================
            // ESTADOS SOLICITUDES
            // ============================================
            modelBuilder.Entity<Estados_Solicitudes>(entity =>
            {
                entity.ToTable("Estados_Solicitudes");
                entity.HasKey(e => e.Id_Estado);
                entity.Property(e => e.Nombre).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Descripcion).HasMaxLength(255);
            });

            // ============================================
            // PERMISOS SOLICITUD
            // ============================================
            modelBuilder.Entity<Permisos_Solicitud>(entity =>
            {
                entity.ToTable("Permisos_Solicitud");
                entity.HasKey(e => e.Id_Permiso);
                entity.Property(e => e.Nombre).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Descripcion).HasMaxLength(255);
            });

            // ============================================
            // PRIORIDAD SOLICITUDES
            // ============================================
            modelBuilder.Entity<Prioridad_Solicitudes>(entity =>
            {
                entity.ToTable("Prioridad_Solicitudes");
                entity.HasKey(e => e.Id_Prioridad);
                entity.Property(e => e.Nombre).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Descripcion).HasMaxLength(255);
            });

            // ============================================
            // ACCIONES SOBRE ARCHIVOS
            // ============================================
            modelBuilder.Entity<Acciones_Sobre_Archivos>(entity =>
            {
                entity.ToTable("Acciones_Sobre_Archivos");
                entity.HasKey(e => e.Id_Accion);
                entity.Property(e => e.Nombre).HasMaxLength(50).IsRequired();
            });

            // ============================================
            // ARCHIVOS
            // ============================================
            modelBuilder.Entity<Archivo>(entity =>
            {
                entity.ToTable("Archivos");
                entity.HasKey(e => e.Id_Archivo);

                entity.Property(e => e.Nombre_Original).HasMaxLength(255).IsRequired();
                entity.Property(e => e.Nombre_Almacenado).HasMaxLength(255).IsRequired();
                entity.Property(e => e.Ruta_Relativa).HasMaxLength(500).IsRequired();
                entity.Property(e => e.Extension).HasMaxLength(10).IsRequired();
                entity.Property(e => e.Tipo_Contenido).HasMaxLength(20).IsRequired();
                entity.Property(e => e.Hash_SHA256).HasMaxLength(64).IsRequired();
                entity.Property(e => e.Ruta_Preview).HasMaxLength(500);
                entity.Property(e => e.Tiene_Preview).HasDefaultValue(false);
                entity.Property(e => e.Estado).HasDefaultValue(true);
                entity.Property(e => e.Fecha_Subida).HasDefaultValueSql("GETDATE()");

                entity.HasOne(e => e.Departamentos)
                      .WithMany(d => d.Archivos)
                      .HasForeignKey(e => e.Codigo_Departamento)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Usuarios)
                      .WithMany(u => u.Archivos)
                      .HasForeignKey(e => e.Subido_Por)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ============================================
            // PLANTILLAS DEPARTAMENTO
            // ============================================
            modelBuilder.Entity<Plantillas_Departamento>(entity =>
            {
                entity.ToTable("Plantillas_Departamento");
                entity.HasKey(e => e.Id_Plantilla);

                entity.Property(e => e.Nombre).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Descripcion).HasMaxLength(255);
                entity.Property(e => e.Activa).HasDefaultValue(true);
                entity.Property(e => e.Fecha_Actualizacion).HasDefaultValueSql("GETDATE()");

                entity.HasOne(e => e.Departamentos)
                      .WithMany(d => d.Plantillas_Departamento)
                      .HasForeignKey(e => e.Codigo_Departamento)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Archivo)
                      .WithOne(a => a.Plantillas_Departamento)
                      .HasForeignKey<Plantillas_Departamento>(e => e.Id_Archivo)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ============================================
            // CONTROL OFICIOS
            // ============================================
            modelBuilder.Entity<Control_Oficios>(entity =>
            {
                entity.ToTable("Control_Oficios");
                entity.HasKey(e => e.Id_Oficio);

                entity.Property(e => e.Numero_Registro).IsRequired();
                entity.Property(e => e.Codigo_De_Referencia).HasMaxLength(50).IsRequired();
                entity.HasIndex(e => e.Codigo_De_Referencia).IsUnique();

                entity.Property(e => e.Dirigido_A).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Copia_A).HasMaxLength(100);
                entity.Property(e => e.Referencia).HasMaxLength(500);
                entity.Property(e => e.Estado_Oficio).HasConversion<string>().HasMaxLength(20).HasDefaultValue(EstadoOficioEnum.Activo);
                entity.Property(e => e.Fecha_Creacion).HasDefaultValueSql("GETDATE()");

                // Relación con Departamento
                entity.HasOne(e => e.Departamento)
                      .WithMany(d => d.Control_Oficios)
                      .HasForeignKey(e => e.Codigo_Departamento)
                      .OnDelete(DeleteBehavior.Restrict);



                // Relación con Archivo
                entity.HasOne(e => e.Archivo)
                      .WithOne(a => a.Control_Oficios)
                      .HasForeignKey<Control_Oficios>(e => e.Id_Archivo)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ============================================
            // SOLICITUDES
            // ============================================
            modelBuilder.Entity<Solicitudes>(entity =>
            {
                entity.ToTable("Solicitudes");
                entity.HasKey(e => e.Id_Solicitud);

                entity.Property(e => e.Justificacion).HasMaxLength(500).IsRequired();
                entity.Property(e => e.Observacion_Respuesta).HasMaxLength(500);
                entity.Property(e => e.Fecha_Solicitud).HasDefaultValueSql("GETDATE()");

                // Solicitante
                entity.HasOne(e => e.Solicitante)
                      .WithMany(u => u.Solicitudes)
                      .HasForeignKey(e => e.Id_Solicitante)
                      .OnDelete(DeleteBehavior.Restrict);



                // Acción solicitada
                entity.HasOne(e => e.Accion)
                      .WithMany(a => a.Solicitudes)
                      .HasForeignKey(e => e.Id_Accion_Solicitada)
                      .OnDelete(DeleteBehavior.Restrict);

                // Prioridad
                entity.HasOne(e => e.Prioridad)
                      .WithMany(p => p.Solicitudes)
                      .HasForeignKey(e => e.Id_Prioridad)
                      .OnDelete(DeleteBehavior.Restrict);

                // Estado
                entity.HasOne(e => e.Estado)
                      .WithMany(es => es.Solicitudes)
                      .HasForeignKey(e => e.Id_Estado)
                      .OnDelete(DeleteBehavior.Restrict);


            });

            // ============================================
            // ACCESOS TEMPORALES
            // ============================================
            modelBuilder.Entity<Accesos_Temporales>(entity =>
            {
                entity.ToTable("Accesos_Temporales");
                entity.HasKey(e => e.Id_Acceso);

                entity.Property(e => e.Fecha_Inicio).HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.Activo).HasDefaultValue(true);



                // Relación con Usuario
                entity.HasOne(e => e.Usuarios)
                      .WithMany(u => u.AccesosTemporales)
                      .HasForeignKey(e => e.Id_Usuario)
                      .OnDelete(DeleteBehavior.Restrict);



                // Relación con Acción
                entity.HasOne(e => e.Acciones_Sobre_Archivos)
                      .WithMany(a => a.AccesosTemporales)
                      .HasForeignKey(e => e.Id_Accion_Permitida)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ============================================
            // AUDITORÍA
            // ============================================
            modelBuilder.Entity<Auditoria_Archivos>(entity =>
            {
                entity.ToTable("Auditoria_Archivos");
                entity.HasKey(e => e.Id_Auditoria);

                entity.Property(e => e.Accion).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Detalle).HasMaxLength(500);
                entity.Property(e => e.Fecha).HasDefaultValueSql("GETDATE()");

                // Relación con Usuario
                entity.HasOne(e => e.Usuario)
                      .WithMany(u => u.Auditorias)
                      .HasForeignKey(e => e.Id_Usuario)
                      .OnDelete(DeleteBehavior.Restrict);

                // Relación con Oficio
                entity.HasOne(e => e.Oficio)
                      .WithMany(o => o.Auditorias)
                      .HasForeignKey(e => e.Id_Oficio)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            // ============================================
            // OFICIOS EN PROGRESO (RESERVAS)
            // ============================================
            modelBuilder.Entity<Oficios_En_Progreso>(entity =>
            {
                entity.ToTable("Oficios_En_Progreso");
                entity.HasKey(e => e.Id_EnProgreso);

                entity.Property(e => e.Codigo_Referencia).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Estado).HasMaxLength(20).HasDefaultValue("EnEdicion");
                entity.Property(e => e.Fecha_Inicio).HasDefaultValueSql("GETDATE()");

                // Relación con Usuario  // ÚNICA relación, no duplicar en Usuarios
                entity.HasOne(e => e.Usuario)
                      .WithMany(u => u.OficiosEnProgreso)
                      .HasForeignKey(e => e.Id_Usuario)
                      .OnDelete(DeleteBehavior.Restrict);

                // Relación con Departamento
                entity.HasOne(e => e.Departamento)
                      .WithMany()
                      .HasForeignKey(e => e.Codigo_Departamento)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ============================================
            // CORRELATIVOS
            // ============================================
            modelBuilder.Entity<Correlativo_Oficio>(entity =>
            {
                entity.ToTable("Correlativos_Oficios");
                entity.HasKey(e => new { e.Codigo_Departamento, e.Año });

                entity.HasOne(e => e.Departamento)
                      .WithMany()
                      .HasForeignKey(e => e.Codigo_Departamento)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}