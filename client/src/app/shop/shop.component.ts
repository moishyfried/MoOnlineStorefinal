import { IType } from './../shared/Models/IType';
import { IBrands } from './../shared/Models/Ibrand';
import { ShopParams } from './../shared/Models/ShopParems';
import { IProduct } from './../shared/Models/Iproducts';
import { ShopService } from './shop.service';
import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { ThrowStmt } from '@angular/compiler';
@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: [ './shop.component.scss' ]
})    
export class ShopComponent implements OnInit {
 @ViewChild('search', { static: false }) searchTerm: ElementRef;
 products: IProduct[];
 brands: IBrands[];
 types: IType[];
 totalCount : number;
 ShopParams :ShopParams;
 sortOptions = [
  { name: 'Alphabetical', value: 'name' },
  { name: 'Price: Low to High', value: 'priceAsc' },
  { name: 'Price: High to Low', value: 'priceDesc' }
  ];
  constructor(private shopService: ShopService) {
    this.ShopParams = shopService.getShopParams() 
  }

  ngOnInit(){
    this.getProducts(true);
    this.getBrands();
    this.getTypes();
 }

 getProducts(usecache = false){
 return  this.shopService.getProducts(usecache).subscribe(Response =>
    {  
      this.products = Response.data;
      this.ShopParams.pageSize = Response.pageSize;
      this.ShopParams.pageNumber = Response.pageIndex;
      this.totalCount = Response.count;
     },
      error => {
        console.log(error);
      });
     }
     getBrands(){
       return this.shopService.getBrands().subscribe(response =>{
         this.brands = [{id: 0,name: 'All'},...response];
       },
       error => {
         console.log(error);
       })
     }
     getTypes(){
      return this.shopService.getTypes().subscribe(response =>{
        this.types = [{id: 0,name: 'All'},...response];
      },error => 
      {
        console.log(error);
      })
    }
    onSortSelected(sort: string){
      this.ShopParams = this.shopService.getShopParams();
      this.ShopParams.sort = sort;
      this.ShopParams.pageNumber = 1;
      this.shopService.setShopParams(this.ShopParams)
      this.getProducts();
    }
    onTypeSelected(typeid : number){
    this.ShopParams = this.shopService.getShopParams();
    this.ShopParams.typeId = typeid;
    this.ShopParams.pageNumber = 1;
    this.shopService.setShopParams(this.ShopParams)
    this.getProducts();
    }
    onBrandSelected(brandid: number){
    this.ShopParams = this.shopService.getShopParams();
      this.ShopParams.brandId = brandid;
      this.ShopParams.pageNumber = 1;
      this.shopService.setShopParams(this.ShopParams)
       this.getProducts();
    }
    onSearch(){
      this.ShopParams = this.shopService.getShopParams();
      this.ShopParams.search= this.searchTerm.nativeElement.value;
      this.ShopParams.pageNumber = 1;
      this.shopService.setShopParams(this.ShopParams)
      this.getProducts();
    }
    onReset(){
      this.searchTerm.nativeElement.value = '';
      this.ShopParams = new ShopParams();
      this.shopService.setShopParams(this.ShopParams)
      this.getProducts();
    }
    onPageChanged(event: any){
      if(this.ShopParams.pageNumber !== event){
       this.ShopParams = this.shopService.getShopParams();
       this.ShopParams.pageNumber = event;
      }
       this.shopService.setShopParams(this.ShopParams)
       this.getProducts(true);
    }
}
