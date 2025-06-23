import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Person, PersonService } from '../../services/person.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertService } from 'src/app/shared/services/alert.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-person-form',
  templateUrl: './person-form.component.html',
  styleUrls: ['./person-form.component.css']
})
export class PersonFormComponent implements OnInit {
  personForm!: FormGroup;
  isEditMode: boolean = false;
  personId?: string;

  constructor(
    private fb: FormBuilder,
    private personService: PersonService,
    private router: Router,
    private route: ActivatedRoute,
    private alertService: AlertService
  ) {}

  private initForm(): void {
    this.personForm = this.fb.group({
      identification: ['', [Validators.required, Validators.maxLength(8), Validators.pattern(/^\d+$/)]],
      fullName: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
      age: [null, [Validators.required, Validators.min(18), Validators.max(110)]],
      gender: ['', [Validators.required]],
      isActive: [false],
      drives: [false],
      usesGlasses: [false],
      isDiabetic: [false],
      otherDiseases: ['', [Validators.maxLength(200)]]
    });
  }

  private loadPerson(): void {
    this.personService.getById(this.personId!).subscribe({
      next: (person) => this.personForm.patchValue(person),
      error: () => this.alertService.error('Error al cargar los datos de la persona')
    });
  }

  onSubmit(): void {
    if (this.personForm.invalid) return;

    const personData: Person = this.personForm.value;

    const request$: Observable<void> = this.isEditMode
      ? this.personService.update(this.personId!, personData)
      : this.personService.create(personData);

    request$.subscribe({
      next: () => {
        this.alertService.success(
          this.isEditMode ? 'Persona actualizada correctamente' : 'Persona registrada correctamente'
        );
        this.router.navigate(['/persons']);
      },
      error: () => this.alertService.error('Ocurrió un error al guardar los datos')
    });
  }

  ngOnInit(): void {
    this.initForm();

    const idParam = this.route.snapshot.paramMap.get('id');
    this.personId = idParam ? idParam : undefined;
    this.isEditMode = !!this.personId;

    if (this.isEditMode) {
      this.loadPerson();
    }
  }
}
