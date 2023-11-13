using AutoMapper;
using Entities.Dtos;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class ProductManager : IProductService{
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

		public ProductManager(IRepositoryManager manager, IMapper mapper)
		{
			_manager = manager;
			_mapper = mapper;
		}

		public void CreateOne(ProductDtoForInsertion productDto)
        {
            Product product = _mapper.Map<Product>(productDto);
            _manager.Product.CreateOneProduct(product);
            _manager.Save();
        }

        public void DeleteOne(int id)
        {
            Product product = GetOne(id, true);
            if(product is not null){
                _manager.Product.DeleteOneProduct(product);
                _manager.Save();    
            }
        }

        public IEnumerable<Product> GetAll(bool trackChanges)
        {
            return _manager.Product.GetAllProducts(trackChanges);
        }

        public Product? GetOne(int id, bool trackChanges)
        {
            return _manager.Product.GetOneProduct(id, trackChanges);
        }

        public ProductDtoForUpdate GetOneForUpdate(int id, bool trackChanges)
        {
            var product = GetOne(id, trackChanges);
            ProductDtoForUpdate productDto = _mapper.Map<ProductDtoForUpdate>(product);
            return productDto;
        }

        public IEnumerable<Product> GetShowcaseProducts(bool trackChanges)
        {
            var products = _manager.Product.GetShowcaseProducts(trackChanges);
            return products;
        }


        public void UpdateOne(ProductDtoForUpdate productDto)
        {
            //var entity = _manager.Product.GetOneProduct(productDto.ProductId, true);
            // entity.ProductName = productDto.ProductName;
            // entity.Price = productDto.Price;
            // entity.CategoryId = productDto.CategoryId;
            var entity = _mapper.Map<Product>(productDto);
            _manager.Product.UpdateOneProduct(entity);
            _manager.Save();
        }
    }
}