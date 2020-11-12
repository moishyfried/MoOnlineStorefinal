import { Router, ActivatedRoute } from '@angular/router';
import { AccountService } from './../account.service';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormGroupName, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
formgroup : FormGroup;
returnurl:string;
  constructor(private accountService :AccountService,private router :Router,private activateroute :ActivatedRoute) { }

  ngOnInit(): void {
    this.returnurl = this.activateroute.snapshot.queryParams.returnUrl || '/shop'
    this.addformcontrols();
  }
addformcontrols(){
  this.formgroup= new FormGroup({
email : new FormControl('',[Validators.required, Validators
        .pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$')]),
password:new FormControl('',[Validators.required])
  })

  }
onsubmit(){
  this.accountService.login(this.formgroup.value).subscribe(() => {
   this.router.navigateByUrl(this.returnurl)
  },error => {
    console.log(error);
  })
}
  goToRgister(){
    this.router.navigate(['../account/register'], {queryParams: {returnUrl: this.returnurl}})
  }
}


