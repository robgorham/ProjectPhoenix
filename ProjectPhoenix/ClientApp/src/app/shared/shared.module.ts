import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ConfirmModalModule } from '../board/confirm-modal/confirm-modal.module';
import { ConfirmModalComponent } from '../board/confirm-modal/confirm-modal.component';


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    ConfirmModalModule
  ],
  exports: [ConfirmModalComponent]
})
export class SharedModule { }
