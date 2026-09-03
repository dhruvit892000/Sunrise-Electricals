import { Component } from '@angular/core';
import { QuoteForm } from '../../shared/components/quote-form';

@Component({
  selector: 'app-contact',
  imports: [QuoteForm],
  templateUrl: './contact.html',
  styleUrl: './contact.css',
})
export class Contact {}
