import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { ActiveOrder } from 'src/app/shared/models/order';
import { AuthService } from 'src/app/shared/services/auth.service';
import { OrderService } from 'src/app/shared/services/order.service';

@Component({
  selector: 'app-active-orders',
  templateUrl: './active-orders.component.html',
  styleUrls: ['./active-orders.component.css']
})
export class ActiveOrdersComponent implements OnInit {
  orders!: ActiveOrder[];
  token: any;
  userId: any;

  constructor(private orderService: OrderService,
              private toastr: ToastrService,
              private authService: AuthService) { }

  ngOnInit() {
    this.getActiveOrder();
  }

  getActiveOrder(): void{
    this.token = localStorage.getItem('token');
    this.userId = this.authService.getUserId(this.token);
    this.orderService.getActiveOrder(this.userId).subscribe(
      data=>{
        this.orders = data as ActiveOrder[];
      }, error =>{
        this.toastr.error("Failed to get any data", 'Error!' , {
          timeOut: 3000,
          closeButton: true,
        });  
      }
    );
  }
}
