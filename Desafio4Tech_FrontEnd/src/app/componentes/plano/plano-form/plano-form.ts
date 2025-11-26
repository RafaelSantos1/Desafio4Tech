import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { PlanoServico } from '../plano.servico';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { Plano } from '../plano.model';
import { ToastrService } from 'ngx-toastr';
import { CommonModule } from '@angular/common';
@Component({
  selector: 'app-plano-form',
  imports: [RouterModule,ReactiveFormsModule,CommonModule],
  templateUrl: './plano-form.html',
  styleUrl: './plano-form.css',
})
export class PlanoForm implements OnInit {
  form!: FormGroup;
  id?: number;

  constructor(
    private fb: FormBuilder,
    private planoServico: PlanoServico,
    private router: Router,
    private route: ActivatedRoute,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.id = +this.route.snapshot.params['id'];

    this.form = this.fb.group({
      nome: ['', Validators.required],
      codigo_registro_ans: ['', Validators.required]
    });

   
    if(this.id){
      this.planoServico.getById(this.id).subscribe(result => this.form.patchValue(result.dados));
    }
  }

  save() {
    if(this.form.invalid) return;

    let plano: Plano = this.form.value;
    plano.id = this.id;
    if (this.id) {

    this.planoServico.update(this.id, plano).subscribe({
      next: (response) => {
        this.toastr.success(response.mensagem, 'Sucesso');
        this.router.navigate([`/planos/${this.id}`]);
      },
      error: (err) => {
          this.toastr.error(err.error.mensagem, 'Erro');
      }
    });

} else {
  this.planoServico.create(plano).subscribe({
    next: (response) => {
     this.toastr.success(response.mensagem, 'Sucesso');
      this.router.navigate(['/planos/']);
    },
    error: (err) => {
        this.toastr.error(err.error.mensagem, 'Erro');
    }
  });
}

  }
}