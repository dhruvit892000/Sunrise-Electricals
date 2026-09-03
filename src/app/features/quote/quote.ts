import { Component, inject } from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
import { ActivatedRoute } from '@angular/router';
import { map } from 'rxjs';
import { QuoteForm } from '../../shared/components/quote-form';

@Component({
  selector: 'app-quote',
  imports: [QuoteForm],
  templateUrl: './quote.html',
  styleUrl: './quote.css',
})
export class Quote {
  private readonly route = inject(ActivatedRoute);

  readonly sku = toSignal(
    this.route.queryParamMap.pipe(map((params) => params.get('sku') ?? '')),
    { initialValue: this.route.snapshot.queryParamMap.get('sku') ?? '' },
  );
}
