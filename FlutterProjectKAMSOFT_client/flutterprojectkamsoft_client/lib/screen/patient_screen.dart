import 'package:flutter/material.dart';
import '../services/api_service.dart';
import '../models/patient.dart';

class PatientsScreen extends StatefulWidget {
  const PatientsScreen({super.key});

  @override
  State<PatientsScreen> createState() => _PatientsScreenState();
}

class _PatientsScreenState extends State<PatientsScreen> {
  final ApiService _apiService = ApiService();

  List<Patient> _patients = [];
  bool _isLoading = true;
  String? _errorMessage;

  @override
  void initState() {
    super.initState();
    _fetchPatients();
  }

  Future<void> _fetchPatients() async {
    setState(() {
      _isLoading = true;
      _errorMessage = null;
    });

    try {
      final patients = await _apiService.getPatients();
      setState(() {
        _patients = patients;
      });
    } catch (e) {
      setState(() {
        _errorMessage = e.toString();
      });
      print('Error fetching patients: $e');
    } finally {
      setState(() {
        _isLoading = false;
      });
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Encrypted Patients'),
        actions: [
          IconButton(
            tooltip: 'Refresh',
            onPressed: _fetchPatients,
            icon: const Icon(Icons.refresh),
          ),
        ],
      ),
      floatingActionButton: FloatingActionButton(
        tooltip: 'Create appointment',
        onPressed: _showCreateAppointmentDialog,
        child: const Icon(Icons.add),
      ),
      body: _buildBody(),
    );
  }

  Future<void> _showCreateAppointmentDialog() async {
    final firstNameController = TextEditingController(text: 'Roman');
    final lastNameController = TextEditingController(text: 'Nahirnyi');
    final pesselController = TextEditingController(text: '453456543');
    final dateOfBirthController = TextEditingController(text: '1998-05-22');
    final titleController = TextEditingController(text: 'Consultation');
    final descriptionController = TextEditingController(text: 'Consultation');
    int disease = 0;

    final createdPatient = await showDialog<Patient>(
      context: context,
      builder: (context) {
        return AlertDialog(
          title: const Text('Create Appointment'),
          content: SingleChildScrollView(
            child: Column(
              mainAxisSize: MainAxisSize.min,
              children: [
                TextField(
                  controller: firstNameController,
                  decoration: const InputDecoration(labelText: 'First name'),
                ),
                TextField(
                  controller: lastNameController,
                  decoration: const InputDecoration(labelText: 'Last name'),
                ),
                TextField(
                  controller: pesselController,
                  keyboardType: TextInputType.number,
                  decoration: const InputDecoration(labelText: 'PESEL'),
                ),
                TextField(
                  controller: dateOfBirthController,
                  decoration: const InputDecoration(labelText: 'Date of birth'),
                ),
                DropdownButtonFormField<int>(
                  value: disease,
                  decoration: const InputDecoration(labelText: 'Disease'),
                  items: const [
                    DropdownMenuItem(value: 0, child: Text('Cancer')),
                    DropdownMenuItem(value: 1, child: Text('Asthma')),
                  ],
                  onChanged: (value) {
                    disease = value ?? 0;
                  },
                ),
                TextField(
                  controller: titleController,
                  decoration: const InputDecoration(labelText: 'Appointment title'),
                ),
                TextField(
                  controller: descriptionController,
                  decoration: const InputDecoration(labelText: 'Appointment description'),
                ),
              ],
            ),
          ),
          actions: [
            TextButton(
              onPressed: () => Navigator.of(context).pop(),
              child: const Text('Cancel'),
            ),
            FilledButton(
              onPressed: () async {
                final patient = await _apiService.createAppointment(
                  firstName: firstNameController.text,
                  lastName: lastNameController.text,
                  pessel: int.parse(pesselController.text),
                  dateOfBirth: DateTime.parse(dateOfBirthController.text),
                  disease: disease,
                  appointmentTitle: titleController.text,
                  appointmentDescription: descriptionController.text,
                );

                if (context.mounted) {
                  Navigator.of(context).pop(patient);
                }
              },
              child: const Text('Create'),
            ),
          ],
        );
      },
    );

    firstNameController.dispose();
    lastNameController.dispose();
    pesselController.dispose();
    dateOfBirthController.dispose();
    titleController.dispose();
    descriptionController.dispose();

    if (createdPatient != null) {
      setState(() {
        _patients.insert(0, createdPatient);
      });
    }
  }

  Widget _buildBody() {
    if (_isLoading) {
      return const Center(child: CircularProgressIndicator());
    }

    if (_errorMessage != null) {
      return Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            Text('Error: $_errorMessage'),
            const SizedBox(height: 16),
            ElevatedButton(
              onPressed: _fetchPatients,
              child: const Text('Retry'),
            ),
          ],
        ),
      );
    }

    if (_patients.isEmpty) {
      return const Center(child: Text('No patients found'));
    }

    return ListView.builder(
      padding: const EdgeInsets.all(12),
      itemCount: _patients.length,
      itemBuilder: (context, index) {
        final patient = _patients[index];
        return Card(
          child: ExpansionTile(
            title: Text(
              patient.name.toString(),
              maxLines: 1,
              overflow: TextOverflow.ellipsis,
            ),
            subtitle: Text(
              'PESEL: ${patient.pessel}',
              maxLines: 1,
              overflow: TextOverflow.ellipsis,
            ),
            childrenPadding: const EdgeInsets.fromLTRB(16, 0, 16, 16),
            children: [
              _EncryptedValue(label: 'Cipher', value: 'RSAEncryption'),
              _EncryptedValue(label: 'Date of birth', value: patient.dateOfBirth),
              _EncryptedValue(label: 'Disease', value: patient.diseaseDescription),
              if (patient.appointments.isNotEmpty) ...[
                const SizedBox(height: 8),
                const Align(
                  alignment: Alignment.centerLeft,
                  child: Text(
                    'Appointments',
                    style: TextStyle(fontWeight: FontWeight.bold),
                  ),
                ),
                const SizedBox(height: 4),
                ...patient.appointments.map(
                  (appointment) => Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      _EncryptedValue(label: 'Title', value: appointment.title),
                      _EncryptedValue(label: 'Description', value: appointment.description),
                      _EncryptedValue(label: 'Type', value: appointment.type),
                      _EncryptedValue(label: 'Date', value: appointment.appointmentDate),
                    ],
                  ),
                ),
              ],
            ],
          ),
        );
      },
    );
  }
}

class _EncryptedValue extends StatelessWidget {
  final String label;
  final String value;

  const _EncryptedValue({
    required this.label,
    required this.value,
  });

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.only(top: 6),
      child: Row(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          SizedBox(
            width: 100,
            child: Text(
              label,
              style: const TextStyle(fontWeight: FontWeight.w600),
            ),
          ),
          Expanded(
            child: SelectableText(
              value,
              maxLines: 3,
            ),
          ),
        ],
      ),
    );
  }
}
