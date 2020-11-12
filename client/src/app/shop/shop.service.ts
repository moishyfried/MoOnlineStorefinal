import { environment } from 'src/environments/environment';
import { Pagination } from './../shared/Models/Ipagination';
import { IProduct } from './../shared/Models/Iproducts';
import { IType } from './../shared/Models/IType';
import { IBrands } from './../shared/Models/Ibrand';
import { ShopParams } from './../shared/Models/ShopParems';
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { IPagination } from '../shared/Models/Ipagination';
import { map, delay } from 'rxjs/operators';
import { of } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class ShopService {
baseurl = environment.apiUrl;
products:IProduct[]= [];
brands :IBrands[] =[];
types:IType[]=[];
shopParams = new  ShopParams();
pagination = new Pagination();

  constructor(private http: HttpClient) {  }

  getProducts(useCache: boolean) {
    let params = new HttpParams();

    if(useCache === false){
      this.products =[];
    }
    if(useCache === true && this.products.length > 0){
     const pagesReicived = Math.ceil(this.products.length / this.shopParams.pageSize);
     if(this.shopParams.pageNumber <= pagesReicived){
        this.pagination.data = this.products.slice(
         (this.shopParams.pageNumber -1) * this.shopParams.pageSize,
         this.shopParams.pageNumber * this.shopParams.pageSize

         ); 
        return of(this.pagination)
      
     }
    }
    if (this.shopParams.brandId !== 0) {
      params = params.append('brandId', this.shopParams.brandId.toString());
    }

    if (this.shopParams.typeId !== 0) {
      params = params.append('typeId', this.shopParams.typeId.toString());
    }

    if (this.shopParams.search) {
      params = params.append('search', this.shopParams.search);
    }
    params = params.append('sort', this.shopParams.sort);
    params = params.append('pageIndex', this.shopParams.pageNumber.toString());
    params = params.append('pageSize', this.shopParams.pageSize.toString());

    return this.http.get<IPagination>(this.baseurl + 'products', { observe: 'response', params })
      .pipe(
        map(response => {
          this.products = [...this.products , ...response.body.data];
          this.pagination = response.body;
          return this.pagination;
        })
      );
  }
  getShopParams(){
    return this.shopParams;
  }
  setShopParams(shopparams :ShopParams){
    this.shopParams = shopparams;
  }
  getProduct(id: number){
    const product = this.products.find(p => p.id === id)
    if(product) return of(product);
    return this.http.get<IProduct>(this.baseurl + 'products/  ' + id)
    }
   getBrands()
   {
     if(this.brands.length > 0){
       return of(this.brands)
     }
   return this.http.get<IBrands[]>(this.baseurl + 'products/brands').pipe(
     map(response => {
       this.brands = response;
       return response;
     })
   );
    

   }
   getTypes()
   {
     if(this.types.length > 0){
       return of(this.types)
     }
   return this.http.get<IType[]>(this.baseurl + 'products/types')
   .pipe(
     map(response => {
       this.types = response;
       return response;
     })
   );
   }
}
