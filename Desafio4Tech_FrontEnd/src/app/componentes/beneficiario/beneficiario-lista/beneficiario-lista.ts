import { Component, OnInit } from '@angular/core';
import { BeneficiarioServico } from '../beneficiario.servico';
import { Beneficiario } from '../beneficiario.model';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-beneficiario-lista',
  imports: [RouterModule,CommonModule],
  templateUrl: './beneficiario-lista.html',
  styleUrl: './beneficiario-lista.css',
})
export class BeneficiarioLista implements OnInit {
  beneficiarios: Beneficiario[] = [];

  constructor(private beneficiarioServico: BeneficiarioServico,
              private toastr: ToastrService) {}

  ngOnInit(): void {
   this.beneficiarios = [];

    this.loadBeneficiarios();
  }

  loadBeneficiarios() {
    this.beneficiarioServico.getAll().subscribe(result => {
      this.beneficiarios = result.dados;
    });
  }

  deleteBeneficiario(id: number) {
    if(confirm('Deseja realmente excluir este beneficiário?')){
       this.beneficiarioServico.delete(id).subscribe({
        next: (response) => {
          this.toastr.success(response.mensagem, 'Sucesso'); 
          this.loadBeneficiarios()
        },
        error: (err) => {
            this.toastr.error(err.error.mensagem, 'Erro');
        }
      })
    }
  }
}
