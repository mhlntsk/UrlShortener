import { Component, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { Url } from '../../shared/Url';

@Component({
  selector: 'app-url',
  standalone: true,
  imports: [
    CommonModule, 
    RouterModule,
  ],
  templateUrl: './url.component.html',
  styleUrl: './url.component.css'
})
export class UrlComponent implements OnInit {
  
  @Input() url!: Url;

  ngOnInit(): void {

  }
}
