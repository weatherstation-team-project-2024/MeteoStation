import { Node } from '../node.model';

export interface NodesResponse {
  nodes: Node[];
  generated_at: number;
}