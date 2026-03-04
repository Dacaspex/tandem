import {useState} from "react";

interface CreateTopicModalProps {
  onSubmit: (topicName: string) => Promise<void>;
  onCancel: () => void;
}

export default function CreateTopicModal({ onSubmit, onCancel }: CreateTopicModalProps) {
  const [topicName, setTopicName] = useState('');

  return (
    <div>
      <div className='mb-0'>Topic name</div>
      <input
        className='form-control form-control-inline mb-1'
        name='topic-name'
        type='text'
        value={ topicName }
        onChange={ (e) => setTopicName(e.target.value) }
      />
      <div className='button-row button-row-reverse'>
        <div
          className='button button-primary button-s'
          onClick={ () => onSubmit(topicName)}>
          Submit
        </div>
        <div
          className='button button-s'
          onClick={ onCancel }>
          Cancel
        </div>
      </div>
    </div>
  )
}