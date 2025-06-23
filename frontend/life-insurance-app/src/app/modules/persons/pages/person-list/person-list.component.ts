import { Component, OnInit } from '@angular/core';
import { Person, PersonService } from '../../services/person.service';
import { AlertService } from 'src/app/shared/services/alert.service';

@Component({
  selector: 'app-person-list',
  templateUrl: './person-list.component.html',
  styleUrls: ['./person-list.component.css']
})
export class PersonListComponent implements OnInit {
  persons: Person[] = [];
  isLoading = false;

   constructor(private personService: PersonService, private alertService: AlertService) {}
  
  ngOnInit(): void {
    this.loadPersons();
  }

  loadPersons(): void {
    this.isLoading = true;
    this.personService.getAll().subscribe({
      next: persons => this.persons = persons,
      error: () => this.alertService.error('Ocurrió un error al cargar los datos'),
      complete: () => this.isLoading = false
    });
  }

  deletePerson(id: string): void {
    this.alertService.confirm('¿Estás seguro que deseas eliminar esta persona?').then((result) => {
      if (result) {
        this.personService.delete(id).subscribe({
          next: () => {
            this.alertService.success('Persona eliminada correctamente');
            this.loadPersons();
          },
          error: () => this.alertService.error('Ocurrió un error al eliminar la persona')
        });
      }
    });
  }
}
