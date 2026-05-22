import 'dart:convert';
import 'package:http/http.dart' as http;
import '../models/patient_result.dart';

class MedicalRepository {
  final String apiUrl = "http://localhost:5150/api/MedicalResults/dashboard";
  static const Map<String, String> encryptionQuery = {
    'CipherType': '2',
  };
  
  Future<List<PatientResult>> fetchDashboardData() async {
    final uri = Uri.parse(apiUrl).replace(queryParameters: encryptionQuery);
    final response = await http.get(uri);

    if (response.statusCode == 200) {
      List<dynamic> body = jsonDecode(response.body);
      return body.map((item) => PatientResult.fromJson(item)).toList();
    } else {
      throw Exception("Backend returned error: ${response.statusCode}");
    }
  }
}
