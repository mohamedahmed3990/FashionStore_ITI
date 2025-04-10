using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashionStore.DAL.Context;
using FashionStore.DAL.Interfaces;
using FashionStore.DAL.Repositories;

namespace FashionStore.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IProductRepository ProductRepo { get; }
        public IProductVariantRepository ProductVariantRepo { get; }
        public IOrderRepository OrderRepo { get; }

        public IColorRepository ColorRepository { get; }
        public ISizeRepository SizeRepository { get; }

        public IParentCategoryRepository ParentCategoryRepository { get; }

        public ISubCategoryRepository SubCategoryRepository { get; }

        public UnitOfWork(IProductRepository productRepo, AppDbContext context,
                          IProductVariantRepository productVariantRepo,
                          IOrderRepository orderRepo, IColorRepository colorRepository, ISizeRepository sizeRepository, ISubCategoryRepository subCategoryRepository
            , IParentCategoryRepository parentCategoryRepository)
        {
            ProductRepo = productRepo;
            _context = context;
            ProductVariantRepo = productVariantRepo;
            OrderRepo = orderRepo;
            ColorRepository = colorRepository;
            SizeRepository = sizeRepository;
            ParentCategoryRepository= parentCategoryRepository;
            SubCategoryRepository= subCategoryRepository;

        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync(); 
        }
    }
}
