import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Article } from 'src/app/shared/models/article';

@Component({
  selector: 'app-article-card',
  templateUrl: './article-card.component.html',
  styleUrls: ['./article-card.component.css']
})
export class ArticleCardComponent implements OnInit {
  @Input() article!: Article;
  quantityForm: FormGroup;
  quantity: any = 1;

  constructor(private fb: FormBuilder,
              private toastr: ToastrService) {
    this.quantityForm = this.fb.group({
      quantityField: [1,Validators.required]
    })
  }
  ngOnInit(): void {
  }

  
  // AddToCart(item: any) : void {
  //    this.quantity = this.quantityForm.get('quantityField')?.value;

  //   var product: any = {
  //     id: item.id,
  //     quantity:this.quantity,
  //     ingredients: item.ingredients,
  //     name: item.name,
  //     price: item.price,
  //     total: item.price*this.quantity
  //   }
  //   //this.cartService.addtoCart(product);
  //   this.toastr.success(this.quantity + ' - ' + item.name + ' have been succesfuly added to your cart' , 'Success!' , {
  //     timeOut: 3000,
  //     closeButton: true,
  //   });
  // }

}
