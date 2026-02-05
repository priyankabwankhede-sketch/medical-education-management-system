import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TraineeService, Trainee } from '../services/trainee.service';
import { RotationService, Rotation } from '../services/rotation.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  // ----------------- TRAINEES -----------------
  trainees: Trainee[] = [];
  username: string = '';
  programId: number = 1;

  // Map for traineeId -> programName
  traineeProgramMap: Record<number, string> = {}; 
  // ----------------- ROTATIONS -----------------
  rotations: Rotation[] = [];
  rotationTraineeId: number = 1;
  startDate: string = '';
  endDate: string = '';

  constructor(
    private traineeService: TraineeService,
    private rotationService: RotationService
  ) {}

  ngOnInit(): void {
    this.loadTrainees();
    this.loadRotations();
  }

  // ----------------- TRAINEE METHODS -----------------
  loadTrainees() {
    this.traineeService.getTrainees().subscribe({
      next: (data: Trainee[]) => {
        this.trainees = data;

        // Populate traineeProgramMap
        this.traineeProgramMap = {};
        this.trainees.forEach(t => {
          this.traineeProgramMap[t.traineeId] = t.programName;
        });
      },
      error: (err: any) => console.error(err)
    });
  }

  createTrainee() {
    this.traineeService.createTrainee({
      username: this.username,
      programId: this.programId
    }).subscribe({
      next: () => {
        this.username = '';
        this.programId = 1;
        this.loadTrainees();
      },
      error: (err: any) => console.error(err)
    });
  }

  deleteTrainee(id: number) {
    if (!confirm('Are you sure you want to delete this trainee?')) return;

    this.traineeService.deleteTrainee(id).subscribe({
      next: () => this.loadTrainees(),
      error: (err: any) => console.error(err)
    });
  }

  // ----------------- ROTATION METHODS -----------------
  loadRotations() {
    this.rotationService.getRotations().subscribe({
      next: (data: Rotation[]) => {
        this.rotations = data;
      },
      error: (err: any) => console.error(err)
    });
  }

  createRotation() {
    if (!this.rotationTraineeId || !this.startDate || !this.endDate) {
      alert('Please fill all fields for rotation.');
      return;
    }

    this.rotationService.createRotation({
      traineeId: this.rotationTraineeId,
      startDate: this.startDate,
      endDate: this.endDate
    }).subscribe({
      next: () => {
        this.rotationTraineeId = 1;
        this.startDate = '';
        this.endDate = '';
        this.loadRotations();
      },
      error: (err: any) => console.error(err)
    });
  }

  deleteRotation(rotationId: number) {
    if (!confirm('Are you sure you want to delete this rotation?')) return;

    this.rotationService.deleteRotation(rotationId).subscribe({
      next: () => this.loadRotations(),
      error: (err: any) => console.error(err)
    });
  }
}
