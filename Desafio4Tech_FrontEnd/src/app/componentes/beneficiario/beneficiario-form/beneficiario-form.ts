import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { BeneficiarioServico } from '../beneficiario.servico';
import { Beneficiario } from '../beneficiario.model';
import { CommonModule } from '@angular/common';
import { PlanoServico } from '../../plano/plano.servico';
import { Plano } from '../../plano/plano.model';

@Component({
  selector: 'app-beneficiario-form',
  imports: [ReactiveFormsModule,RouterModule,CommonModule],
  templateUrl: './beneficiario-form.html',
  styleUrl: './beneficiario-form.css',
})
export class BeneficiarioForm implements OnInit {

  form!: FormGroup;
  id: number = 0;
  planos: Plano[] = [];
  dataLimite: string = ""; 
  constructor(
    private fb: FormBuilder,
    private beneficiarioServico: BeneficiarioServico,
    private planoServico: PlanoServico,
    private router: Router,
    private route: ActivatedRoute,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    const hoje = new Date();
    this.dataLimite = hoje.toISOString().substring(0, 10);
    this.id = +this.route.snapshot.params['id'];

    this.form = this.fb.group({
      nome_completo: ['', Validators.required],
     cpf: [{ value: '', disabled: this.id > 0 }, Validators.required],
      data_nascimento: [Date, Validators.required],
      status: ['', Validators.required],
      idPlano: ['', Validators.required]
    });
    
    this.planoServico.getAll().subscribe(response => { this.planos = response.dados });

    if (this.id) {
      this.beneficiarioServico.getById(this.id).subscribe(result => {
        result.dados.data_nascimento = new Date(result.dados.data_nascimento).toISOString().substring(0, 10);
        this.form.patchValue(result.dados);
      });
    }
  }

  save() {
    if (this.form.invalid) return;
    if (this.id) {
       let beneficiario: Beneficiario = this.form.getRawValue();
       beneficiario.id = this.id;

      this.beneficiarioServico.update(this.id, beneficiario).subscribe({
        next: (response) => {
          if(response.status){
            this.toastr.success(response.mensagem, 'Sucesso');
            this.router.navigate([`/beneficiarios/${this.id}`]);
          }
          else
            this.toastr.error(response.mensagem, 'Erro');
        },
        error: (err) => {
          this.toastr.error(err.error.mensagem, 'Erro');
        }
      });
    } else {
      let beneficiario: Beneficiario = this.form.value;
      beneficiario.id = this.id;

      this.beneficiarioServico.create(beneficiario).subscribe({
        next: (response) => {
          if(response.status){
            this.toastr.success(response.mensagem, 'Sucesso');
            this.router.navigate(['/beneficiarios']);
          }
          else
            this.toastr.error(response.mensagem, 'Erro');
        },
        error: (err) => {
          this.toastr.error(err.error.mensagem, 'Erro');
        }
      });
    }
  }

}
