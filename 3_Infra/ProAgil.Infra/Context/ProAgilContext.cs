using System;
using System.Linq;
using FluentValidator;
using FluentValidator.Validation;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain.ProAgilContext.Entities;
using ProAgil.Domain.ValueObjects;
using ProAgil.Shared.Entities;

namespace ProAgil.Infra.Context
{
    public class ProAgilContext : DbContext
    {
        public ProAgilContext(DbContextOptions<ProAgilContext> options) 
            : base(options){
        }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<Palestrante> Palestrantes { get; set; }
        public DbSet<RedeSocial> RedeSociais { get; set; }
        public DbSet<PalestranteEvento> PalestranteEventos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            
            modelBuilder.Ignore<Notifiable>();
            modelBuilder.Ignore<Notification>();
            modelBuilder.Ignore<Email>();

            modelBuilder.Entity<PalestranteEvento>()
                     .HasKey(ev => new { ev.EventoId, ev.PalestranteId});            

            modelBuilder.Entity<PalestranteEvento>()
                .HasOne(p => p.Evento)
                .WithMany(b => b.PalestranteEventos)
                .HasForeignKey(bc => bc.EventoId);  

           modelBuilder.Entity<PalestranteEvento>()
                .HasOne(p => p.Palestrante)
                .WithMany(b => b.PalestranteEventos)
                .HasForeignKey(bc => bc.PalestranteId);

            EntityMapping(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }


        private void EntityMapping(ModelBuilder modelBuilder){
                modelBuilder.Entity<Palestrante>(entity =>  
                {  
                    entity.ToTable("Palestrante")
                    .HasKey(e => e.Id); 
                    
                    entity.Property(e => e.identifyer).HasDefaultValueSql("lower(hex(randomblob(16)))");
                    entity.Property(e => e.Nome)
                        .HasMaxLength(100);  

                    entity.Property(e => e.MiniCurriculo)
                        .IsRequired()  
                        .HasMaxLength(100)
                        .HasColumnName("MiniCurriculo");

                    entity.Property(e => e.ImgUrl)  
                        .IsRequired()  
                        .HasMaxLength(100)
                        .HasColumnName("ImgUrl");

                    entity.Property(e => e.Email)
                        .IsRequired()
                        .HasMaxLength(50).
                        HasColumnName("Email");
                });  
            modelBuilder.Entity<Evento>(entity =>  
                {  
                    entity.ToTable("Evento").HasKey(e => e.Id);   
                    entity.Property(e => e.identifyer).HasDefaultValueSql("lower(hex(randomblob(16)))");
                    entity.Property(e => e.Local)  
                        .HasMaxLength(100).
                        HasColumnName("Local");

                    entity.Property(e => e.Tema)  
                        .IsRequired()  
                        .HasMaxLength(100).
                        HasColumnName("Tema");

                    entity.Property(e => e.ImgUrl)  
                        .IsRequired()  
                        .HasMaxLength(100).
                        HasColumnName("ImgUrl");

                    entity.Property(e => e.QtdPessoas)  
                        .IsRequired().
                        HasColumnName("QtdPessoas");

                    entity.Property(e => e.Email)
                        .IsRequired()
                        .HasMaxLength(50).
                        HasColumnName("Email");
                });  

            modelBuilder.Entity<PalestranteEvento>(entity =>  
                {  
                    entity.ToTable("PalestranteEvento").HasKey(e => e.Id);  
                    entity.Property(e => e.identifyer).HasDefaultValueSql("lower(hex(randomblob(16)))");
            });  
            modelBuilder.Entity<Lote>(entity =>  
                {  
                    entity.ToTable("Lote").HasKey(e => e.Id);   
                    entity.Property(e => e.identifyer).HasDefaultValueSql("lower(hex(randomblob(16)))");

                    entity.Property(e => e.Nome)  
                        .HasMaxLength(100).
                        HasColumnName("Nome");

                    entity.Property(e => e.Preco)  
                        .IsRequired().
                        HasColumnName("Preco");

                    entity.Property(e => e.Quantidade)  
                        .IsRequired().
                        HasColumnName("Quantidade");

                });

            modelBuilder.Entity<RedeSocial>(entity =>  
                {  
                    entity.ToTable("RedeSocial").HasKey(e=>e.Id);

                    entity.Property(e => e.identifyer).HasDefaultValueSql("lower(hex(randomblob(16)))");
                    entity.Property(e => e.Nome)
                        .HasMaxLength(100);  
                    entity.Property(e => e.Url)
                        .HasMaxLength(100);  
            });
        }
    }
}