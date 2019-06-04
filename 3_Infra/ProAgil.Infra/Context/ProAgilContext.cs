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
                    entity.ToTable("Palestrante");  
                    entity.Property(e => e.Id).HasDefaultValueSql("lower(hex(randomblob(16)))");

                    
                    entity.Property(e => e.Nome)  
                        .HasMaxLength(100)  
                        .IsUnicode(false);  
                    entity.Property(e => e.MiniCurriculo)  
                        .IsRequired()  
                        .HasMaxLength(100)  
                        .IsUnicode(false);  
                    entity.Property(e => e.ImgUrl)  
                        .IsRequired()  
                        .HasMaxLength(100)  
                        .IsUnicode(false);  
                    entity.Property(e => e.Email)  
                        .IsRequired()  
                        .HasMaxLength(50)  
                        .IsUnicode(false); 
            });  
            modelBuilder.Entity<Evento>(entity =>  
                {  
                    entity.ToTable("Evento");  
                    entity.Property(e => e.Id).HasDefaultValueSql("lower(hex(randomblob(16)))");
                    entity.Property(e => e.Local)  
                        .HasMaxLength(100)  
                        .IsUnicode(false);  
                    entity.Property(e => e.Tema)  
                        .IsRequired()  
                        .HasMaxLength(100)  
                        .IsUnicode(false);  
                    entity.Property(e => e.ImgUrl)  
                        .IsRequired()  
                        .HasMaxLength(100)  
                        .IsUnicode(false);  
                    entity.Property(e => e.QtdPessoas)  
                        .IsRequired()  
                        .IsUnicode(false);  
                    entity.Property(e => e.Email)  
                        .IsRequired()  
                        .HasMaxLength(50)  
                        .IsUnicode(false);
            });  

            modelBuilder.Entity<PalestranteEvento>(entity =>  
                {  
                    entity.ToTable("PalestranteEvento");  
                    entity.Property(e => e.Id).HasDefaultValueSql("lower(hex(randomblob(16)))");
            });  
            modelBuilder.Entity<Lote>(entity =>  
                {  
                    entity.ToTable("Lote");  
                    entity.Property(e => e.Id).HasDefaultValueSql("lower(hex(randomblob(16)))");
                    entity.Property(e => e.Nome)  
                        .HasMaxLength(100)  
                        .IsUnicode(false);  
                    entity.Property(e => e.Preco)  
                        .IsRequired()  
                        .IsUnicode(false);  
                    entity.Property(e => e.Quantidade)  
                        .IsRequired()  
                        .IsUnicode(false);  
            });

            modelBuilder.Entity<RedeSocial>(entity =>  
                {  
                    entity.ToTable("RedeSocial");  

                    entity.Property(e => e.Id).HasDefaultValueSql("lower(hex(randomblob(16)))");

                    entity.Property(e => e.Nome)  
                        .HasMaxLength(100)  
                        .IsUnicode(false);  

                    entity.Property(e => e.Url)  
                        .HasMaxLength(100)  
                        .IsUnicode(false);  
            });
        }
    }
}