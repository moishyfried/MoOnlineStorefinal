import { Router, ActivatedRoute } from '@angular/router';
import { AccountService } from './../account.service';
import { FormGroup, FormBuilder, Validators, AsyncValidatorFn } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { timer, of } from 'rxjs';
import { switchMap, map } from 'rxjs/operators';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  constructor(private fb :FormBuilder,private accountservice :AccountService,private router:Router,private activateroute:ActivatedRoute) { }
registerform : FormGroup;
returnurl:string;
errors :string[];
  ngOnInit(): void {
this.returnurl = this.activateroute.snapshot.queryParams.returnUrl || '/shop';
this.createregisterform();
  }
  createregisterform(){
    this.registerform = this.fb.group({
      displayname : ['',Validators.required],
      email : ['',[Validators.required,Validators.
      pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$')],
       [this.validateemailnottaken() ]],
       password : ['',Validators.required]})};

onsubmit(){
  this.accountservice.register(this.registerform.value).subscribe(response => {
    this.router.navigateByUrl(this.returnurl) },
    error => {
      console.log(error)
      this.errors = error.errors
    })
}

validateemailnottaken():AsyncValidatorFn{
return control => {
  return timer(500).pipe(
    switchMap(() => {
      if(!control.value)
      {return of(null);
      }
     
      return this.accountservice.checkemailexists(control.value).pipe(
        map(response => {
          return response ? {emailexists:true} : null
        })
      );
    })
  )
}
}
} 
