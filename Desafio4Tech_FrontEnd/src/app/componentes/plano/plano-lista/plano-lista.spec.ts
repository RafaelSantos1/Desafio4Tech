import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PlanoLista } from './plano-lista';

describe('PlanoLista', () => {
  let component: PlanoLista;
  let fixture: ComponentFixture<PlanoLista>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PlanoLista]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PlanoLista);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
